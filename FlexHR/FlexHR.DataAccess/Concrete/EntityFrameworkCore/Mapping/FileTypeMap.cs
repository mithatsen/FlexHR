using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class FileTypeMap : IEntityTypeConfiguration<FileType>
    {
        public void Configure(EntityTypeBuilder<FileType> entity)
        {
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.FileUploadTypeName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
        }

    }
}
