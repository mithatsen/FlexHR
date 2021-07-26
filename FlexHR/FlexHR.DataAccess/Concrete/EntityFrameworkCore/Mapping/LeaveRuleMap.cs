using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class LeaveRuleMap : IEntityTypeConfiguration<LeaveRule>
    {
        public void Configure(EntityTypeBuilder<LeaveRule> entity)
        {
            entity.HasKey(e => e.RuleId);
            entity.Property(e => e.AditionalLeaveAmount)
            .IsRequired();

            entity.Property(e => e.SeniorityYear)
            .IsRequired();
            entity.Property(e => e.IsActive)
            .IsRequired();
        }
    }
}
