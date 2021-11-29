using Core.Entites.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Contexts
{
    public class UcsanContext : DbContext
    {
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public UcsanContext() 
        {

        }

        public UcsanContext(DbContextOptions<UcsanContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseSqlServer(@"Server=176.53.69.151\MSSQLSERVER2019;Database=sevgiler_ucsan;user id=sevgiler_ucsan;password=V0vi3a6%;");
                optionsBuilder.UseSqlServer(connectionString: @"Server=SA-DENIZSEVGI;Database=UcsanSogutma;Trusted_Connection=true;");
            }

            
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Reference> Reference { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactForm> ContactForm { get; set; }
        public virtual DbSet<TeklifForm> TeklifForm { get; set; }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OperationClaim> OperationClaims { get; set; }
        public virtual DbSet<UserOperationClaim> UserOperationClaims { get; set; }



    }
}
