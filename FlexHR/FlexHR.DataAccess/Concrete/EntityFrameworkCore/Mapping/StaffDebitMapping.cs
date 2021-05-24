using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffDebitMapping : IEntityTypeConfiguration<StaffDebit>
    {
        public void Configure(EntityTypeBuilder<StaffDebit> entity)
        {
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.IssueDate).HasColumnType("datetime");

            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffDebit)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffDebit_Staff");
        }
    }
}
