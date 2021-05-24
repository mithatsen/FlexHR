using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffFileMap : IEntityTypeConfiguration<StaffFile>
    {
        public void Configure(EntityTypeBuilder<StaffFile> entity)
        {
            entity.ToTable("Staff_File");

            entity.Property(e => e.FileFullPath)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffFile)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_File_Staff");

        }
    }
}
