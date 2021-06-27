using Microsoft.Extensions.Configuration;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoakDAXPWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure

            // Map entities to table
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<FlightEventType>().ToTable("flight_event_type");
            modelBuilder.Entity<Flight>().ToTable("flight");

            // Configure Primary Keys
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("pk_user");
            modelBuilder.Entity<FlightEventType>().HasKey(fet => fet.Id).HasName("pk_flight_event_type");
            modelBuilder.Entity<Flight>().HasKey(f => f.Id).HasName("pk_flight");

            // Configure indexes
            // User
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique().HasDatabaseName("idx_username_unique");
            modelBuilder.Entity<User>().HasIndex(u => u.Email).HasDatabaseName("idx_email");
            modelBuilder.Entity<User>().HasIndex(u => u.FirstName).HasDatabaseName("idx_first_name");
            modelBuilder.Entity<User>().HasIndex(u => u.LastName).HasDatabaseName("idx_last_name");
            // Flight Event Type
            modelBuilder.Entity<FlightEventType>().HasIndex(fet => fet.Name).IsUnique().HasDatabaseName("idx_flight_event_name_unique");

            // Configure columns
            
            //User
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Username).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnType("varchar(255)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnType("varchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnType("longtext").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreatedDate).HasColumnType("datetime(6)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UpdatedDate).HasColumnType("datetime(6)").IsRequired(false);

            //Flight Event Type
            modelBuilder.Entity<FlightEventType>().Property(fet => fet.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<FlightEventType>().Property(fet => fet.Name).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<FlightEventType>().Property(fet => fet.Enabled).HasColumnType("tinyint(1)").IsRequired();
            modelBuilder.Entity<FlightEventType>().Property(fet => fet.CreatedDate).HasColumnType("datetime(6)").IsRequired();
            modelBuilder.Entity<FlightEventType>().Property(fet => fet.UpdatedDate).HasColumnType("datetime(6)").IsRequired(false);

            //Flight
            modelBuilder.Entity<Flight>().Property(f => f.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Flight>().Property(f => f.EventDateTime).HasColumnType("datetime(6)").IsRequired();
            modelBuilder.Entity<Flight>().Property(f => f.Location).HasColumnType("varchar(100)").IsRequired();
            modelBuilder.Entity<Flight>().Property(f => f.Latitude).HasColumnType("double").IsRequired();
            modelBuilder.Entity<Flight>().Property(f => f.Longitude).HasColumnType("double").IsRequired();
            modelBuilder.Entity<Flight>().Property(f => f.DistanceFromIdeal).HasColumnType("double").IsRequired(false);
            modelBuilder.Entity<Flight>().Property(f => f.GlideslopeScore).HasColumnType("double").IsRequired(false);
            modelBuilder.Entity<Flight>().Property(f => f.VerticalSpeed).HasColumnType("double").IsRequired(false);
            modelBuilder.Entity<Flight>().Property(f => f.MaxForce).HasColumnType("double").IsRequired(false);
            modelBuilder.Entity<Flight>().Property(f => f.Pitch).HasColumnType("double").IsRequired(false);
            modelBuilder.Entity<Flight>().Property(f => f.CreatedDate).HasColumnType("datetime(6)").IsRequired();
            modelBuilder.Entity<Flight>().Property(f => f.UpdatedDate).HasColumnType("datetime(6)").IsRequired(false);

            // Relationships
            //modelBuilder.Entity<Flight>().HasOne<FlightEventType>().WithMany().HasPrincipalKey(fet => fet.Id).HasForeignKey(f => f.FlightEventTypeId).OnDelete(DeleteBehavior.Cascade).HasConstraintName(string.Format("fk_{0}", Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Override method to automatically set CreatedDate and UpdatedDate
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }

        public DbSet<FlightEventType> FlightEventTypes { get; set; }
    }
}
