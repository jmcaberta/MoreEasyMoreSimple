using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Sales
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sale")
                .HasKey(s => s.saleId);
        }
    }
}
