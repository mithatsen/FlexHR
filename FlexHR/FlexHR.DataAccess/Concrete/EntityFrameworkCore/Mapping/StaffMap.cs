using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffMap : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> entity)
        {
            entity.Property(e => e.EmailJob).HasMaxLength(50);

            entity.Property(e => e.EmailPersonal).HasMaxLength(50).IsRequired();

            entity.Property(e => e.JobFinishDate).HasColumnType("datetime");

            entity.Property(e => e.JobJoinDate).HasColumnType("datetime");

            entity.Property(e => e.NameSurname)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneJob).HasMaxLength(15);

            entity.Property(e => e.PhonePersonal).HasMaxLength(15).IsRequired();
       
           
        }
    }
}
