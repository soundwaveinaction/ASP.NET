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
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ServiceInfo)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.BeginDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder.Property(x => x.PartnerName)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(p => p.PartnerManager)
                .WithMany(e => e.PromoCodes)
                .HasForeignKey(p => p.PartnerManagerId);

            builder.HasOne(p => p.Customer)
                .WithMany(c => c.PromoCodes)
                .HasForeignKey(p => p.CustomerId);

            builder.HasOne(p => p.Preference)
                .WithMany()
                .HasForeignKey(p => p.PreferenceId);

        }
    }
}
