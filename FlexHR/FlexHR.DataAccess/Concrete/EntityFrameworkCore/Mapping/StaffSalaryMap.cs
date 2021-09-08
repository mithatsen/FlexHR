using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class StaffSalaryMap : IEntityTypeConfiguration<StaffSalary>
    {
        public void Configure(EntityTypeBuilder<StaffSalary> entity)
        {
            entity.Property(e => e.ConstantBonus).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.FoodPayment).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.IsAgi).HasColumnName("IsAGI");

            entity.Property(e => e.PrivateHealthCare).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.PrivatePension).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.RoadPayment).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.NetSalary).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.GrossSalary).HasColumnType("numeric(18, 2)");

            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffSalary)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffSalary_Staff");

        }
    }
}
