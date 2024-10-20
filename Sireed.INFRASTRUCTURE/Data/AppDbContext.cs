﻿using Microsoft.EntityFrameworkCore;
using Sireed.API.Models;

namespace Sireed.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Actualite> Actualites { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Indicateur> indicateurs { get; set; }
        public DbSet<Partenaire> Partenaires { get; set; }
        public DbSet<Document> Documents { get; set; } 
        public DbSet<Region> Regions { get; set; }
        public DbSet<ProjetRecherche> ProjetRecherches { get; set; }
    }
}
