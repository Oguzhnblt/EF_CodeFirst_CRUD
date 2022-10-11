using EF_CodeFirst_CRUD.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_CodeFirst_CRUD.DAL.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.OrderDetailId);
            builder.HasOne(t0 => t0.Order).WithMany(t1 => t1.OrderDetails).HasForeignKey(t2 => t2.OrderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t0 => t0.Product).WithMany(t1 => t1.OrderDetails).HasForeignKey(t2 => t2.ProductId).OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
