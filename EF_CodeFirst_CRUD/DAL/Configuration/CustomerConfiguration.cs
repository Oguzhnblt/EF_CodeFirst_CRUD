using EF_CodeFirst_CRUD.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_CRUD.DAL.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(t0 => t0.CustomerName).HasMaxLength(255);
            builder.Property(t0 => t0.Phone).HasMaxLength(255);
            builder.Property(t0 => t0.Email).HasMaxLength(255);
            builder.Property(t0 => t0.Adress).HasMaxLength(2000);

            // One to Zero ilişki
            builder.HasOne(t0 => t0.Country).WithMany().HasForeignKey(t1 => t1.CountryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t0 => t0.City).WithMany().HasForeignKey(t1 => t1.CityId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
