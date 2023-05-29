using Jedlik.EntityFramework.Helper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Varga_Balázs_Ádám_backend.Models
{
    public class Ingatlan
    {
        [Unique]
        public int id { get; set; }
        [Required]
        public Kategoria kategoria { get; set; }
        public string leiras { get; set; }
        public DateTime hirdetesDatuma { get; set; } = DateTime.Now;
        [Required]
        public bool tehermentes { get; set; }
        [Required]
        public int ar { get; set; }
        public string kepUrl { get; set; }
    }
}
