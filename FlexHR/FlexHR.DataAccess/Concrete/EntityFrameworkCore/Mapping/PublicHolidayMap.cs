using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class PublicHolidayMap : IEntityTypeConfiguration<PublicHoliday>
    {
        public void Configure(EntityTypeBuilder<PublicHoliday> entity)
        {
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.FinishDate).HasColumnType("datetime");

            entity.Property(e => e.HolidayDuration).HasColumnType("numeric(3, 1)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.StartDate).HasColumnType("datetime");
        }
    }
}
