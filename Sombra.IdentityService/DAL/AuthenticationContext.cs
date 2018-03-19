using System;
using Microsoft.EntityFrameworkCore;

namespace Sombra.IdentityService.DAL
{
    public class AuthenticationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("AUTHENTICATION_DB_CONNECTIONSTRING"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new LanguageEntityTypeConfiguration());
            
        }

    }
}