using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Warehouse
{
    public class AdmissionMap : IEntityTypeConfiguration<Admission>
    {
        public void Configure(EntityTypeBuilder<Admission> builder)
        {
            builder.ToTable("admission")
                .HasKey(a => a.admissionId);          
        }
    }
}
