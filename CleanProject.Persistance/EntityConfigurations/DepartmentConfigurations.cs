using System;
using CleanProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistance.EntityConfigurations
{
    public class DepartmentConfigurations
    {
        public class EmployeeConfigurations : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.HasKey(x => x.DepartmentId);

                builder.Property(x => x.DepartmentId).UseIdentityColumn(1, 1);
                   
                builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(1000);

                builder.HasMany(x => x.Employees)
                    .WithOne(x => x.Department);               
            }
        }
    }
}

