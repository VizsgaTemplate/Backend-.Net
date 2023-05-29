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
- <img src="https://github.com/VizsgaTemplate/Backend-.Net/video/Import-from-csv.gif" width="400" height="400" />
