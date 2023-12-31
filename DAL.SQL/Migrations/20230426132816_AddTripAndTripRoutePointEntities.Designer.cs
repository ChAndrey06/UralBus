﻿// <auto-generated />
using System;
using DAL.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.SQL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230426132816_AddTripAndTripRoutePointEntities")]
    partial class AddTripAndTripRoutePointEntities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.AuthCode.AuthCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuthCodes");
                });

            modelBuilder.Entity("DAL.Entities.KontragentIdentity.KontragentIdentity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContractEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContractPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Kontragents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("KontragentIdentity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DAL.Entities.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ArrivalPointId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CarrierId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DeparturePointId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("PassengerCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SettlePointId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalPointId");

                    b.HasIndex("CarrierId");

                    b.HasIndex("ClientId");

                    b.HasIndex("DeparturePointId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("SettlePointId");

                    b.ToTable("Orders");

                    b.HasDiscriminator<string>("CreationType").HasValue("Web");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DAL.Entities.Payment.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ReturnId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ReturnId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.PersonIdentity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PersonIdentity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DAL.Entities.Return.Return", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReturnType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("DAL.Entities.RoutePoint.RoutePoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoutePoints");
                });

            modelBuilder.Entity("DAL.Entities.Transport.Transport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CarrierId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("DAL.Entities.Trip.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TravelTime")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("DAL.Entities.Trip.TripRoutePoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RoutePointType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoutePointtId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TriptId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoutePointtId");

                    b.HasIndex("TriptId");

                    b.ToTable("TripRoutePoints");
                });

            modelBuilder.Entity("DAL.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Entities.KontragentIdentity.CarrierIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.KontragentIdentity.KontragentIdentity");

                    b.HasDiscriminator().HasValue("Carrier");
                });

            modelBuilder.Entity("DAL.Entities.KontragentIdentity.InsuranceIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.KontragentIdentity.KontragentIdentity");

                    b.HasDiscriminator().HasValue("Insurance");
                });

            modelBuilder.Entity("DAL.Entities.Order.OperatorOrder", b =>
                {
                    b.HasBaseType("DAL.Entities.Order.Order");

                    b.Property<Guid>("OperatorId")
                        .HasColumnType("uuid");

                    b.HasIndex("OperatorId");

                    b.HasDiscriminator().HasValue("Operator");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.ClientPersonIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.PersonIdentity.PersonIdentity");

                    b.Property<bool>("Blacklisted")
                        .HasColumnType("boolean");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.DispatcherPersonIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.PersonIdentity.PersonIdentity");

                    b.HasDiscriminator().HasValue("Dispatcher");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.DriverPersonIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.PersonIdentity.PersonIdentity");

                    b.Property<Guid>("CarrierId")
                        .HasColumnType("uuid");

                    b.HasIndex("CarrierId");

                    b.HasDiscriminator().HasValue("Driver");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.OperatorPersonIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.PersonIdentity.PersonIdentity");

                    b.HasDiscriminator().HasValue("Operator");
                });

            modelBuilder.Entity("DAL.Entities.AuthCode.AuthCode", b =>
                {
                    b.HasOne("DAL.Entities.User.User", "User")
                        .WithMany("AuthCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.Order.Order", b =>
                {
                    b.HasOne("DAL.Entities.RoutePoint.RoutePoint", "ArrivalPoint")
                        .WithMany()
                        .HasForeignKey("ArrivalPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.KontragentIdentity.CarrierIdentity", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.PersonIdentity.ClientPersonIdentity", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.RoutePoint.RoutePoint", "DeparturePoint")
                        .WithMany()
                        .HasForeignKey("DeparturePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Payment.Payment", "Payment")
                        .WithOne()
                        .HasForeignKey("DAL.Entities.Order.Order", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.RoutePoint.RoutePoint", "SettlePoint")
                        .WithMany()
                        .HasForeignKey("SettlePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalPoint");

                    b.Navigation("Carrier");

                    b.Navigation("Client");

                    b.Navigation("DeparturePoint");

                    b.Navigation("Payment");

                    b.Navigation("SettlePoint");
                });

            modelBuilder.Entity("DAL.Entities.Payment.Payment", b =>
                {
                    b.HasOne("DAL.Entities.Return.Return", "Return")
                        .WithOne()
                        .HasForeignKey("DAL.Entities.Payment.Payment", "ReturnId");

                    b.Navigation("Return");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.PersonIdentity", b =>
                {
                    b.HasOne("DAL.Entities.User.User", "User")
                        .WithMany("Identities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.Transport.Transport", b =>
                {
                    b.HasOne("DAL.Entities.KontragentIdentity.CarrierIdentity", null)
                        .WithMany("Transports")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.Trip.TripRoutePoint", b =>
                {
                    b.HasOne("DAL.Entities.RoutePoint.RoutePoint", "RoutePoint")
                        .WithMany()
                        .HasForeignKey("RoutePointtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Trip.Trip", "Trip")
                        .WithMany("RoutePoints")
                        .HasForeignKey("TriptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoutePoint");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("DAL.Entities.Order.OperatorOrder", b =>
                {
                    b.HasOne("DAL.Entities.PersonIdentity.OperatorPersonIdentity", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.DriverPersonIdentity", b =>
                {
                    b.HasOne("DAL.Entities.KontragentIdentity.CarrierIdentity", null)
                        .WithMany("Drivers")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.Trip.Trip", b =>
                {
                    b.Navigation("RoutePoints");
                });

            modelBuilder.Entity("DAL.Entities.User.User", b =>
                {
                    b.Navigation("AuthCodes");

                    b.Navigation("Identities");
                });

            modelBuilder.Entity("DAL.Entities.KontragentIdentity.CarrierIdentity", b =>
                {
                    b.Navigation("Drivers");

                    b.Navigation("Transports");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.ClientPersonIdentity", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
