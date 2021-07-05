using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class ReceiptRuleMap : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> entity)
        {
            entity.Property(e => e.FileFullPath)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Vat)
                .IsRequired();
            entity.Property(e => e.Amount)
                .IsRequired();
            entity.Property(e => e.IsActive)
                .IsRequired();
            entity.Property(e => e.Name)
                .IsRequired();
            entity.HasOne(d => d.StaffPayment)
                .WithMany(p => p.Receipts)
                .HasForeignKey(d => d.StaffPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffPayment_Receipt");
        }
    }
}
