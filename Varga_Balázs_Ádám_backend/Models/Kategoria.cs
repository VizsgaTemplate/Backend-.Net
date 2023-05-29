using Jedlik.EntityFramework.Helper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Varga_Balázs_Ádám_backend.Models
{
    public class Kategoria
    {
        [Unique]
        public int id { get; set; }
        [Unique, Required]
        public string nev { get; set; }
    }
}
