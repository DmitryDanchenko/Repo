using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReWeight.Models
{
    public class ApplicationContext :DbContext
    {
        public DbSet<ControlPoint> controlPoints { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Links> Links { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
