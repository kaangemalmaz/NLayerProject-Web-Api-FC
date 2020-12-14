using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Entity.Models;

namespace UdemyNLayerProject.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);

            builder.ToTable("Categories");
        }
    }
}
