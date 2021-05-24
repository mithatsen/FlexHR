using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffGeneralSubTypeMapping : IEntityTypeConfiguration<StaffGeneralSubType>
    {
        public void Configure(EntityTypeBuilder<StaffGeneralSubType> entity)
        {
            entity.HasKey(e => e.GeneralSubTypeStaffId);

            entity.ToTable("Staff_GeneralSubType");

            entity.HasOne(d => d.GeneralSubType)
                .WithMany(p => p.StaffGeneralSubType)
                .HasForeignKey(d => d.GeneralSubTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_GeneralSubType_GeneralSubType");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffGeneralSubType)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_GeneralSubType_Staff");

        }
    }
}
