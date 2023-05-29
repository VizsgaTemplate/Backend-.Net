using Jedlik.EntityFramework.Helper;
using Microsoft.EntityFrameworkCore;
using Varga_Balázs_Ádám_backend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varga_Balázs_Ádám_backend
{
    //Parancsok a Package manager console-ban:
    //  Add-Migration <név> -Project <data project> -StartupProject <data project>
    //  Script-Migration -Project <data project> -StartupProject <data project>
    //  update-database -Project <data project> -StartupProject <data project>

    public class DataContext : JedlikDbContext
    {
        public DbSet<Ingatlan> ingatlanok { get; set; }
        public DbSet<Kategoria> kategoriak { get; set; }

        private readonly string connStr;

        public DataContext()
        {
            connStr = "server=localhost;database=ingatlan;uid=root;pwd=;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connStr))
                optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Kategoria>().HasData
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
}