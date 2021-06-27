﻿// <auto-generated />
using System;
using JoakDAXPWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JoakDAXPWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210627182444_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("JoakDAXPWebApp.Entities.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("DistanceFromIdeal")
                        .HasColumnType("double");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FlightEventTypeId")
                        .HasColumnType("int");

                    b.Property<double?>("GlideslopeScore")
                        .HasColumnType("double");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<double?>("MaxForce")
                        .HasColumnType("double");

                    b.Property<double?>("Pitch")
                        .HasColumnType("double");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("VerticalSpeed")
                        .HasColumnType("double");

                    b.HasKey("Id")
                        .HasName("pk_flight");

                    b.HasIndex("FlightEventTypeId");

                    b.ToTable("flight");
                });

            modelBuilder.Entity("JoakDAXPWebApp.Entities.FlightEventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("pk_flight_event_type");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("idx_flight_event_name_unique");

                    b.ToTable("flight_event_type");
                });

            modelBuilder.Entity("JoakDAXPWebApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Email")
                        .HasDatabaseName("idx_email");

                    b.HasIndex("FirstName")
                        .HasDatabaseName("idx_first_name");

                    b.HasIndex("LastName")
                        .HasDatabaseName("idx_last_name");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("idx_username_unique");

                    b.ToTable("user");
                });

            modelBuilder.Entity("JoakDAXPWebApp.Entities.Flight", b =>
                {
                    b.HasOne("JoakDAXPWebApp.Entities.FlightEventType", "FlightEventType")
                        .WithMany("Flights")
                        .HasForeignKey("FlightEventTypeId");

                    b.Navigation("FlightEventType");
                });

            modelBuilder.Entity("JoakDAXPWebApp.Entities.FlightEventType", b =>
                {
                    b.Navigation("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}