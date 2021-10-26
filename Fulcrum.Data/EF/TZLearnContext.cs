﻿using Fulcrum.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Data.EF
{
    public sealed class TzLearnContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public TzLearnContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TZLearnDb;Trusted_Connection=True;");
        }
    }
}