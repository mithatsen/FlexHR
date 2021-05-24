using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffCareerMap : IEntityTypeConfiguration<StaffCareer>
    {
        public void Configure(EntityTypeBuilder<StaffCareer> entity)
        {
            entity.Property(e => e.JobFinishDate).HasColumnType("datetime");

            entity.Property(e => e.JobStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.CompanyBranch)
                .WithMany(p => p.StaffCareer)
                .HasForeignKey(d => d.CompanyBranchId)
                .HasConstraintName("FK_StaffCareer_CompanyBranch");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffCareer)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffCareer_Staff");
        }
    }
}
