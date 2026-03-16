using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId);

            builder.Property(e => e.AppliedPromocodesCount)
                .IsRequired();

        }
    }
}
