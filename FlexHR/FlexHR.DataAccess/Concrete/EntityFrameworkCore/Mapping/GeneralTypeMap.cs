using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class GeneralTypeMap : IEntityTypeConfiguration<GeneralType>
    {
        public void Configure(EntityTypeBuilder<GeneralType> entity)
        {
            entity.Property(e => e.Description).HasMaxLength(200);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
