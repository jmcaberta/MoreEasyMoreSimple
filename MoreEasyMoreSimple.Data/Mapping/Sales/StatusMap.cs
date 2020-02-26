using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Sales
{
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("status")
                .HasKey(s => s.statusId);            
        }
    }
}
