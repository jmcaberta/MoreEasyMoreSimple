using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Users
{
    public class UserMap : IEntityTypeConfiguration<Username>
    {
        public void Configure(EntityTypeBuilder<Username> builder)
        {
            builder.ToTable("username")
                .HasKey(u => u.userId);
        }
    }
}
