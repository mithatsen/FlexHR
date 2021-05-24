using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CompanyBranchMap : IEntityTypeConfiguration<CompanyBranch>
    {
        public void Configure(EntityTypeBuilder<CompanyBranch> entity)
        {
            entity.Property(e => e.BranchName)
                   .IsRequired()
                   .HasMaxLength(100);

            entity.HasOne(d => d.Company)
                .WithMany(p => p.CompanyBranch)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyBranch_Company");
        }
    }
}
