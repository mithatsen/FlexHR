using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffOtherInfoMap : IEntityTypeConfiguration<StaffOtherInfo>
    {
        public void Configure(EntityTypeBuilder<StaffOtherInfo> entity)
        {
            entity.Property(e => e.AccountNo)
                    .HasMaxLength(30);
                    

            entity.Property(e => e.Adress).HasMaxLength(2000);

            entity.Property(e => e.BankName).HasMaxLength(100);

            entity.Property(e => e.CallPersonNameSurname).HasMaxLength(50);

            entity.Property(e => e.CallPersonPhoneNumber).HasMaxLength(15);

            entity.Property(e => e.CallPersonProximityDegree).HasMaxLength(50);

            entity.Property(e => e.HomePhoneNumber).HasMaxLength(15);

            entity.Property(e => e.Iban)
                .HasColumnName("IBAN")
                .HasMaxLength(30);

            entity.Property(e => e.PostalCode).HasMaxLength(10);

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffOtherInfo)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffOtherInfo_Staff");

            entity.HasOne(d => d.Town)
                .WithMany(p => p.StaffOtherInfo)
                .HasForeignKey(d => d.TownId)
                .HasConstraintName("FK_StaffOtherInfo_Town");

        }
    }
}
