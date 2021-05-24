using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffPersonelInfoMap : IEntityTypeConfiguration<StaffPersonelInfo>
    {
        public void Configure(EntityTypeBuilder<StaffPersonelInfo> entity)
        {
            entity.Property(e => e.BirthDate).HasColumnType("datetime");

            entity.Property(e => e.IdNumber).HasMaxLength(20);

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffPersonelInfo)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffPersonelInfo_Staff");

        }
    }
}
