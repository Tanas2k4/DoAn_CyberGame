using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class CyBerModel : DbContext
    {
        public IEnumerable<object> Sessions;

        public CyBerModel()
            : base("name=CyBerModel1")
        {
        }

        public virtual DbSet<FoodItem> FoodItems { get; set; }
        public virtual DbSet<FoodStock> FoodStocks { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<MachineSession> MachineSessions { get; set; }
        public virtual DbSet<Maintenance> Maintenances { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        //public virtual DbSet<RevenueReport> RevenueReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItem>()
                .HasMany(e => e.FoodStocks)
                .WithRequired(e => e.FoodItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodItem>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.FoodItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Machine>()
                .HasMany(e => e.MachineSessions)
                .WithRequired(e => e.Machine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Machine>()
                .HasMany(e => e.Maintenances)
                .WithRequired(e => e.Machine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MachineSession>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.MachineSession)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FoodItems)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Invoices)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Machines)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Machines1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<Zone>()
                .HasMany(e => e.Machines)
                .WithRequired(e => e.Zone)
                .WillCascadeOnDelete(false);
        }
    }
}
