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
    public class CityConfiguration : IEntityTypeConfiguration<City>

    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.CityId);
            builder.Property(t0 => t0.CityName).HasMaxLength(255);
            
            // Bire-çok ilişki
            builder.HasOne(t0 => t0.Country).WithMany(t1 => t1.Cities).HasForeignKey(t2 => t2.CountryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new City() { CityId = 1, CityName = "Adana", CountryId = 1 },
                new City() { CityId = 2, CityName = "Adıyaman", CountryId = 1 },
                new City() { CityId = 3, CityName = "Afyon", CountryId = 1 },
                new City() { CityId = 4, CityName = "Ağrı", CountryId = 1 },
                new City() { CityId = 5, CityName = "Amasya", CountryId = 1 },
                new City() { CityId = 6, CityName = "Ankara", CountryId = 1 }
                
                );
                            
        }

    }
}
