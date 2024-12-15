using System;
using CleanProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistance.EntityConfigurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.EmployeeId);

            builder.Property(x => x.EmployeeId).UseIdentityColumn(1, 1);

            builder.Property(x => x.FirstName)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .IsRequired();
        }
    }
}

