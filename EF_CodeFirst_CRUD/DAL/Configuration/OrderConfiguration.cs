using EF_CodeFirst_CRUD.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_CRUD.DAL.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder.HasKey(x => x.OrderId);
            builder.Property(t0 => t0.OrderCode).HasMaxLength(100);
            builder.HasOne(t0 => t0.Customer).WithMany(t0 => t0.Orders).HasForeignKey(t0 => t0.CustomerId).OnDelete(DeleteBehavior.Restrict);
            // onDeleteAction => 
        }
    }
}
