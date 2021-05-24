using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class EmailHistoryMap : IEntityTypeConfiguration<EmailHistory>
    {
        public void Configure(EntityTypeBuilder<EmailHistory> entity)
        {
            entity.Property(e => e.AttachmentFileFullPath).HasMaxLength(1000);

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.EmailCc)
                .HasColumnName("EmailCC")
                .HasMaxLength(300);

            entity.Property(e => e.EmailFrom)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.EmailSubject)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.EmailTo)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
