using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class GeneralSubTypeMap : IEntityTypeConfiguration<GeneralSubType>
    {
        public void Configure(EntityTypeBuilder<GeneralSubType> entity)
        {
            entity.Property(e => e.Description).HasMaxLength(100);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.GeneralType)
                .WithMany(p => p.GeneralSubType)
                .HasForeignKey(d => d.GeneralTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralSubType_GeneralType");
        }
    }
}
