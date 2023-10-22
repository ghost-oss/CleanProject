using System;
using System.Reflection;
using CleanProject.Domain.Common;
using CleanProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Persistance.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)));
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var trackedEntry = ChangeTracker.Entries<AuditableEntity>();

            foreach (var entry in trackedEntry)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Default User";
                        entry.Entity.CreatedDate = DateTime.Today;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "Default User";
                        entry.Entity.LastModifiedDate = DateTime.Today;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}

