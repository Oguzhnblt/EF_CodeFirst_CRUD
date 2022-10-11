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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(t0 => t0.CountryId);
            builder.Property(t0 => t0.CountryName).HasMaxLength(255);

            // Seed Data => Database oluştuktan sonra bu datalar tablo içerisinde oluşsun

            builder.HasData(
                new Country() { CountryId = 1, CountryName = "Türkiye" },
                new Country() { CountryId = 2, CountryName = "Rusya" },
                new Country() { CountryId = 3, CountryName = "USA" });
        }
    }
}
