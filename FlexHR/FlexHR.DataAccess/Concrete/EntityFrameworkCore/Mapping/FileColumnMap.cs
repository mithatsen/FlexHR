using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class FileColumnMap : IEntityTypeConfiguration<FileColumn>
    {
        public void Configure(EntityTypeBuilder<FileColumn> entity)
        {
            entity.Property(e => e.ColumnName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.DataType).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ColumnDescription).HasMaxLength(200).IsRequired(false);
            entity.Property(e => e.ColumnSequence).IsRequired();
            entity.Property(e => e.AllowNulls).IsRequired();
            entity.Property(e => e.IsExistControl).IsRequired();
            entity.Property(e => e.IsExistInExcel).IsRequired();
            entity.Property(e => e.IsManuellAdded).IsRequired();
            entity.HasOne(d => d.FileType)
              .WithMany(p => p.FileColumn)
              .HasForeignKey(d => d.FileTypeId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_FileColumn_FileType");
        }
    }
}
