using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class FileColumnPropertiesMap : IEntityTypeConfiguration<FileColumnProperties>
    {
        public void Configure(EntityTypeBuilder<FileColumnProperties> entity)
        {
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.PropertyName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.PropertyDesc).IsRequired(false);
            entity.Property(e => e.IsActive).IsRequired();
        }
    }
}
