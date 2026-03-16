using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(x => x.PromoCodes)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.Id);

            builder.HasMany(x => x.Preferences)
                .WithMany(x => x.Customers)
                .UsingEntity(j => j.ToTable("CustomerPreference"));
        }
    }
}
