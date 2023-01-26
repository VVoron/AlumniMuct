﻿using AlumniMuctr.Models;
using Microsoft.EntityFrameworkCore;

namespace AlumniMuctr.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        public DbSet<News> News { get; set; }
        public DbSet<Programms> Programms { get; set; }
        public DbSet<RegistrationForm> RegistrationForm { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
