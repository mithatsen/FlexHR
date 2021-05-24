using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffShiftMapping : IEntityTypeConfiguration<StaffShift>
    {
        public void Configure(EntityTypeBuilder<StaffShift> entity)
        {
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffShift)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffShift_Staff");

        }
    }
}
