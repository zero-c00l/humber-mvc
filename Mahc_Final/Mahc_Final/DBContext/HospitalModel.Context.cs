﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mahc_Final.DBContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitalContext : DbContext
    {
        public HospitalContext()
            : base("name=HospitalContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<contactu> contactus { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ERArchive> ERArchives { get; set; }
        public virtual DbSet<ERParam> ERParams { get; set; }
        public virtual DbSet<ERWaitTime> ERWaitTimes { get; set; }
        public virtual DbSet<faq> faqs { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<GiftCat> GiftCats { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<HosMember> HosMembers { get; set; }
        public virtual DbSet<Job_applications> Job_applications { get; set; }
        public virtual DbSet<Job_types> Job_types { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Staff1> Staff1 { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
    }
}
