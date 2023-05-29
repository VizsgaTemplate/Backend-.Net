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
        [HttpGet ("api/ingatlan")]
        public IActionResult GetIngatlanok()
        {
            return Ok(dataContext.ingatlanok
                .Include(i => i.kategoria)
                .Select(i => new
                {
                    i.id,
                    kategoria = i.kategoria.nev,
                    i.leiras,
                    hirdetesDatuma = i.hirdetesDatuma.ToString("yyyy-MM-dd"),
                    i.tehermentes,
                    i.ar,
                    i.kepUrl
                }));
        }

        [HttpPost("api/ingatlan")]
        public IActionResult CreateIngatlan(CreateIngatlanRequest data)
        {
            try
            {
                Ingatlan model = new Ingatlan()
                {
                    id = data._id,
                    kategoria = dataContext.kategoriak.First<Kategoria>(k => k.id == data.kategoria),
                    leiras = data.leiras,
                    hirdetesDatuma = data.hirdetesDatuma,
                    tehermentes = data.tehermentes,
                    ar = data.ar,
                    kepUrl = data.kepUrl
                };
                dataContext.ingatlanok.Add(model);
                dataContext.SaveChanges();
                return StatusCode(201, new { model.id });
            }
            catch
            {
                return BadRequest("Hiányos adatok.");
            }
        }

        [HttpDelete("api/ingatlan/{id}")]
        public IActionResult DeleteIngatlan(int id)
        {
            var model = dataContext.ingatlanok.FirstOrDefault(x => x.id == id);
            if (model is null)
                return NotFound("Az ingatlan nem létezik.");

            dataContext.ingatlanok.Remove(model);
            dataContext.SaveChanges();
            return NoContent();
        }
    }
}