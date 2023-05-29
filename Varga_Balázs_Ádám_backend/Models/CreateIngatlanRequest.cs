using Jedlik.EntityFramework.Helper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Varga_Balázs_Ádám_backend.Models
{
    public class CreateIngatlanRequest
    {
        public int _id { get; set; }
        [Required(ErrorMessage ="Hiányos adatok")]
        public int kategoria { get; set; }
        [Required(ErrorMessage ="Hiányos adatok")]
        public string leiras { get; set; }
        [Required(ErrorMessage ="Hiányos adatok")]
        public DateTime hirdetesDatuma { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="Hiányos adatok")]
        public bool tehermentes { get; set; }
        [Required(ErrorMessage ="Hiányos adatok")]
        public int ar { get; set; }
        [Required(ErrorMessage ="Hiányos adatok")]
        public string kepUrl { get; set; }
      }
    

}
