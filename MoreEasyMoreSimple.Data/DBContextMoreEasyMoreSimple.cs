using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data.Mapping.Sales;
using MoreEasyMoreSimple.Data.Mapping.Users;
using MoreEasyMoreSimple.Data.Mapping.Warehouse;
using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Entities.Users;
using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Data
{
    public class DBContextMoreEasyMoreSimple : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Username> Usernames { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Admissiondetails> Admissiondetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Saledetails> Saledetails { get; set; }
        public DBContextMoreEasyMoreSimple(DbContextOptions<DBContextMoreEasyMoreSimple> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new StatusMap());
            modelBuilder.ApplyConfiguration(new BranchMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new AdmissionMap());
            modelBuilder.ApplyConfiguration(new AdmissiondetailsMap());
            modelBuilder.ApplyConfiguration(new SaleMap());
            modelBuilder.ApplyConfiguration(new SaledetailsMap());
        }
    }
}
