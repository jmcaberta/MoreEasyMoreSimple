using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data.Mapping.Warehouse
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("article")
                .HasKey(a => a.articleId);
        }
    }
}
