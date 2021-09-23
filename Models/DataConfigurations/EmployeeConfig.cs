using DeliverIT.Models.DatabaseModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIT.Models.DataConfigurations
{
    class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasIndex(e => e.AddressId, "IX_Employees_AddressId");

            builder.HasOne(d => d.Address)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.AddressId);


            builder.Property(e => e.Email).IsRequired();          

            builder.HasIndex(e => e.Email).IsUnique();                       
            
            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();
        }        
    }
}
