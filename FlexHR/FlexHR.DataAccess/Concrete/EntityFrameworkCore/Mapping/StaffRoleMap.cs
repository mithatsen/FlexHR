using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
   
    public class StaffRoleMap : IEntityTypeConfiguration<StaffRole>
    {
        public void Configure(EntityTypeBuilder<StaffRole> builder)
        {
            builder.ToTable("Staff_Role");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.StaffRole)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Role_Role");

            builder.HasOne(d => d.Staff)
                .WithMany(p => p.StaffRole)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Role_Staff");


        }
    }
}
