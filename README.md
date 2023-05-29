# Lépések
- Telepítsd BP Jedlik.EntityFramework.Helper nugetpackage-t!
- Írd meg az osztályaidat használva a ``[Unique]``, ``[Required]`` jellemzőket.
- Csinálj egy DataContext-et, ami valahogy így néz ki:
```
public class DataContext : JedlikDbContext
    {
        public DbSet<OSZTÁLY1> táblázatneve1 { get; set; }
        public DbSet<OSZTÁLY2> táblázatneve2 { get; set; }

        private readonly string connStr;

        public DataContext()
        {
            connStr = "server=localhost;database=ADATBÁZISNEVE;uid=root;pwd=;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connStr))
                optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OSZTÁLY2>().HasData //feltöltendő adatok kódból
            (
                new() { id = 1, nev = "Ház" },
                new() { id = 2, nev = "Lakás" },
                new() { id = 3, nev = "Építési telek" },
                new() { id = 4, nev = "Garázs" },
                new() { id = 5, nev = "Mezőgazdasági terület" },
                new() { id = 6, nev = "Ipari ingatlan" }
            );

        }
    }
```

Futtasd ezt a három sort a Package Manager Consoleban (Menüsáv->View->Other Windows->Package Manager Console)
```
  Add-Migration Initial
  update-database
  Script-Migration
```

Majd a létre jött adatbázis scriptbe írd ezt az elejére:
 ```
 CREATE DATABASE ADATBÁZISNEVE
	CHARACTER SET utf8mb4
	COLLATE utf8mb4_hungarian_ci;

use ADATBÁZISNEVE
 ```
És mentsd el a **projekt mappájába**!

- Importáld be csv-ből az adatokat így:
<img src="https://github.com/VizsgaTemplate/Backend-.Net/blob/main/video/Import-from-csv.gif"/>

- Csinálj egy kontrollert, pl:
```
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Varga_Balázs_Ádám_backend.Models;

namespace Varga_Balázs_Ádám_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        DataContext dataContext=new();
        [HttpGet ("api/ADATBÁZISNEVE")]
        public IActionResult GetAll()
        {
            return Ok(dataContext.táblázatneve1
                .Include(i => i.masikOsztalyProperty) //pl rendeles.vevo
                .Select(i => new
                {
                    i.id,
                    aNevAhogyMegjelenjen = i.masikOsztalyProperty.nev, // Nem az egész osztályt, hanem csak a példány nevét jeleníti meg (pl rendeles.vevo.nev)
                    i.randomJellemzo,
                    datum = i.datum.ToString("yyyy-MM-dd"),
		    //...
                }));
        }

        [HttpPost("api/ADATBÁZISNEVE")]
        public IActionResult Create(CreateRequest data) //Ezt elmagyarázom következőnek
        {
            try
            {
                Ingatlan model = new Ingatlan()
                {
                    id = data._id,
                    masikOsztalyProperty = dataContext.táblázatneve2.First<OSZTÁLY2>(k => k.id == data.masikOsztalyProperty),
                    randomJellemzo = data.randomJellemzo,
		    //...
                };
                dataContext.táblázatneve1.Add(model);
                dataContext.SaveChanges();
                return StatusCode(201, new { model.id });
            }
            catch
            {
                return BadRequest("Hiányos adatok.");
            }
        }

        [HttpDelete("api/ADATBÁZISNEVE/{id}")]
        public IActionResult Delete(int id)
        {
            var model = dataContext.táblázatneve1.FirstOrDefault(x => x.id == id);
            if (model is null)
                return NotFound("Az blabla nem létezik.");

            dataContext.táblázatneve1.Remove(model);
            dataContext.SaveChanges();
            return NoContent();
        }
    }
}
```

- Írd meg az említett CreateRequest modellt, ami csak egy osztály, amiben szerepelnek a szükséges propertyk. Pl.:
```
        public int _id { get; set; }
        [Required]
        public int masikOsztalyProperty { get; set; }
        [Required]
        public string randomJellemzo1 { get; set; }
        [Required]
        public DateTime randomJellemzo2 { get; set; } = DateTime.Now;
        [Required]
        public bool randomJellemzo3 { get; set; }
        [Required]
        public int randomJellemzo4 { get; set; }
        [Required]
        public string randomJellemzo5 { get; set; }
```

- Csináld meg a Postman cuccokat, ne felejtsd el POST-nál átállítani a body type-ot JSON-re TEXT-ről.
- Töröld a bin, obj mappákat!
