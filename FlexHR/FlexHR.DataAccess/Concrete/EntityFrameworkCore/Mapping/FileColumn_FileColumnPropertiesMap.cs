using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class FileColumn_FileColumnPropertiesMap : IEntityTypeConfiguration<FileColumn_FileColumnProperties>
    {
        public void Configure(EntityTypeBuilder<FileColumn_FileColumnProperties> entity)
        {
            entity.Property(x => x.Id).UseIdentityColumn();
            entity.HasOne(d => d.FileColumn)
             .WithMany(p => p.FileColumn_FileColumnProperties)
             .HasForeignKey(d => d.FileColumnId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_FileColumnFileColumnProperties_FileColumn");  
            entity.HasOne(d => d.FileColumnProperties)
             .WithMany(p => p.FileColumn_FileColumnProperties)
             .HasForeignKey(d => d.FileColumnPropertiesId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_FileColumnFileColumnProperties_FileColumnProperties");
        }

    }
}
