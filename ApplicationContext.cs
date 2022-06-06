using System;
using Microsoft.EntityFrameworkCore;
using ABCBankContacts.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ABCBankContacts
{
    public class ApplicationContext:DbContext
    {
        public string DbPath { get; }
        public DbSet<Contact> Contacts { get; set; }
        public ApplicationContext() {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ContactDB.db");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ContactDB.db;");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    modelBuilder.Entity<Contact>().ToTable("Contacts");
        //}

    }
}
