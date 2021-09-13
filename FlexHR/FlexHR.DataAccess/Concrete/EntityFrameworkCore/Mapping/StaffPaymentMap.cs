using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffPaymentMap : IEntityTypeConfiguration<StaffPayment>
    {
        public void Configure(EntityTypeBuilder<StaffPayment> entity)
        {
            entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.WhoApprovedStaffId).IsRequired(false);

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffPayment)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffPayment_Staff");

        }
    }
}
