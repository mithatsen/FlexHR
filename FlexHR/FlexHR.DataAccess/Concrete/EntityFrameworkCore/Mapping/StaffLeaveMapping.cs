using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffLeaveMapping : IEntityTypeConfiguration<StaffLeave>
    {
        public void Configure(EntityTypeBuilder<StaffLeave> entity)
        {
            entity.Property(e => e.Description).HasMaxLength(1000);

            entity.Property(e => e.LeaveEndDate).HasColumnType("datetime");

            entity.Property(e => e.LeaveStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffLeave)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffLeave_Staff");

        }
    }
}
