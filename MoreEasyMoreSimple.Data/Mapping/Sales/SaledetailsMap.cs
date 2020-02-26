using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Sales
{
    public class SaledetailsMap : IEntityTypeConfiguration<Saledetails>
    {
        public void Configure(EntityTypeBuilder<Saledetails> builder)
        {
            builder.ToTable("saledetails")
                .HasKey("saledetailId");
        }
    }
}
