﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaleManagementSystem.Model.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Customers> Tbl_Customers { get; set; }
        public virtual DbSet<Tbl_OrderDetails> Tbl_OrderDetails { get; set; }
        public virtual DbSet<Tbl_Orders> Tbl_Orders { get; set; }
        public virtual DbSet<Tbl_OrderStatus> Tbl_OrderStatus { get; set; }
        public virtual DbSet<Tbl_ProductCategories> Tbl_ProductCategories { get; set; }
        public virtual DbSet<Tbl_Products> Tbl_Products { get; set; }
        public virtual DbSet<Tbl_UserRoles> Tbl_UserRoles { get; set; }
        public virtual DbSet<Tbl_Users> Tbl_Users { get; set; }
    }
}
