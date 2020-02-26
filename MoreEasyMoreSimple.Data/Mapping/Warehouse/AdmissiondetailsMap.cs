using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Warehouse
{
    public class AdmissiondetailsMap : IEntityTypeConfiguration<Admissiondetails>
    {
        public void Configure(EntityTypeBuilder<Admissiondetails> builder)
        {
            builder.ToTable("admissiondetails")
                .HasKey(a => a.admissiondetId);
        }
    }
}
