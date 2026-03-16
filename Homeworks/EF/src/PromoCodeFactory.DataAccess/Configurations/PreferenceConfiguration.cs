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
    public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
    {
        public void Configure(EntityTypeBuilder<Preference> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
