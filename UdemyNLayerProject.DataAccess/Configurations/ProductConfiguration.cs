using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Entity.Models;

namespace UdemyNLayerProject.DataAccess.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Stock).IsRequired().HasMaxLength(200);

            builder.Property(i => i.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(i => i.InnerBarcode).HasMaxLength(50);

            builder.ToTable("Products");
        }
    }
}
