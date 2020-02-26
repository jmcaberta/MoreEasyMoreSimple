using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Sales
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("contact")
                .HasKey(c => c.contactId);
        }
    }
}
