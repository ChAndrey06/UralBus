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
    [Migration("20230511170923_addPopular")]
    partial class addPopular
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

            modelBuilder.Entity("DAL.Entities.FAQ.FAQ", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FAQCategoryId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FAQCategoryId");

                    b.ToTable("FAQs");
                });

            modelBuilder.Entity("DAL.Entities.FAQ.FAQCategory", b =>
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FAQCategories");
                });

            modelBuilder.Entity("DAL.Entities.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UploaderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UploaderId");

                    b.ToTable("Files");

                    b.HasDiscriminator<string>("Discriminator").HasValue("File");

                    b.UseTphMappingStrategy();
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

            modelBuilder.Entity("DAL.Entities.News.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("DAL.Entities.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClientPersonIdentityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ClientPersonIdentityId");

                    b.ToTable("Orders");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Order");

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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

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

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Address = "Москва, Россия",
                            CreatedAt = new DateTime(2023, 5, 11, 20, 9, 23, 586, DateTimeKind.Utc).AddTicks(4900),
                            IsDeleted = false,
                            Latitude = "55.755826",
                            Longitude = "37.6172999",
                            Title = "Москва"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Address = "Санкт-Петербург, Россия",
                            CreatedAt = new DateTime(2023, 5, 11, 20, 9, 23, 586, DateTimeKind.Utc).AddTicks(4930),
                            IsDeleted = false,
                            Latitude = "59.9342802",
                            Longitude = "30.3350986",
                            Title = "Санкт-Петербург"
                        });
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

                    b.Property<Guid?>("DriverId")
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

                    b.HasIndex("DriverId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("DAL.Entities.Trip.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CarrierId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("TransportId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TripRouteId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("DriverId");

                    b.HasIndex("TransportId");

                    b.HasIndex("TripRouteId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("DAL.Entities.TripRoute.TripRoute", b =>
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

                    b.Property<bool>("PopularDestination")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("PopularDestinationPictureId")
                        .HasColumnType("uuid");

                    b.Property<string>("PopularDestinatonDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PopularDestinationPictureId");

                    b.ToTable("TripRoute");
                });

            modelBuilder.Entity("DAL.Entities.TripRoute.TripRoutePoint", b =>
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

                    b.Property<int>("MinutesFromStart")
                        .HasColumnType("integer");

                    b.Property<decimal>("PriceForPoint")
                        .HasColumnType("numeric");

                    b.Property<Guid>("RoutePointId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TripRouteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("RoutePointType");

                    b.HasKey("Id");

                    b.HasIndex("RoutePointId");

                    b.HasIndex("TripRouteId");

                    b.ToTable("TripRoutePoints");
                });

            modelBuilder.Entity("DAL.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sex")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Entities.Files.Base64File", b =>
                {
                    b.HasBaseType("DAL.Entities.Files.File");

                    b.Property<string>("Base64Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Base64");
                });

            modelBuilder.Entity("DAL.Entities.Files.S3File", b =>
                {
                    b.HasBaseType("DAL.Entities.Files.File");

                    b.Property<string>("Bucket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("S3");
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

                    b.HasDiscriminator().HasValue("OperatorOrder");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.ClientPersonIdentity", b =>
                {
                    b.HasBaseType("DAL.Entities.PersonIdentity.PersonIdentity");

                    b.Property<bool>("Blacklisted")
                        .HasColumnType("boolean");

                    b.Property<string>("IdentificationDocumentNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdentificationDocumentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("SendAdvertisements")
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

                    b.Property<Guid?>("CarrierId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

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

            modelBuilder.Entity("DAL.Entities.FAQ.FAQ", b =>
                {
                    b.HasOne("DAL.Entities.FAQ.FAQCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.FAQ.FAQCategory", null)
                        .WithMany("FAQs")
                        .HasForeignKey("FAQCategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DAL.Entities.Files.File", b =>
                {
                    b.HasOne("DAL.Entities.User.User", "Uploader")
                        .WithMany()
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("DAL.Entities.News.News", b =>
                {
                    b.HasOne("DAL.Entities.Files.Base64File", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.Navigation("File");
                });

            modelBuilder.Entity("DAL.Entities.Order.Order", b =>
                {
                    b.HasOne("DAL.Entities.PersonIdentity.ClientPersonIdentity", null)
                        .WithMany("Orders")
                        .HasForeignKey("ClientPersonIdentityId");
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
                    b.HasOne("DAL.Entities.KontragentIdentity.CarrierIdentity", "Carrier")
                        .WithMany("Transports")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.PersonIdentity.DriverPersonIdentity", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.Navigation("Carrier");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("DAL.Entities.Trip.Trip", b =>
                {
                    b.HasOne("DAL.Entities.KontragentIdentity.CarrierIdentity", "Carrier")
                        .WithMany("Trips")
                        .HasForeignKey("CarrierId");

                    b.HasOne("DAL.Entities.PersonIdentity.DriverPersonIdentity", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("DriverId");

                    b.HasOne("DAL.Entities.Transport.Transport", "Transport")
                        .WithMany()
                        .HasForeignKey("TransportId");

                    b.HasOne("DAL.Entities.TripRoute.TripRoute", "TripRoute")
                        .WithMany()
                        .HasForeignKey("TripRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");

                    b.Navigation("Driver");

                    b.Navigation("Transport");

                    b.Navigation("TripRoute");
                });

            modelBuilder.Entity("DAL.Entities.TripRoute.TripRoute", b =>
                {
                    b.HasOne("DAL.Entities.Files.Base64File", "PopularDestinationPicture")
                        .WithMany()
                        .HasForeignKey("PopularDestinationPictureId");

                    b.Navigation("PopularDestinationPicture");
                });

            modelBuilder.Entity("DAL.Entities.TripRoute.TripRoutePoint", b =>
                {
                    b.HasOne("DAL.Entities.RoutePoint.RoutePoint", "RoutePoint")
                        .WithMany()
                        .HasForeignKey("RoutePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.TripRoute.TripRoute", "TripRoute")
                        .WithMany("TripRoutePoints")
                        .HasForeignKey("TripRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoutePoint");

                    b.Navigation("TripRoute");
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
                    b.HasOne("DAL.Entities.KontragentIdentity.CarrierIdentity", "Carrier")
                        .WithMany("Drivers")
                        .HasForeignKey("CarrierId");

                    b.Navigation("Carrier");
                });

            modelBuilder.Entity("DAL.Entities.FAQ.FAQCategory", b =>
                {
                    b.Navigation("FAQs");
                });

            modelBuilder.Entity("DAL.Entities.TripRoute.TripRoute", b =>
                {
                    b.Navigation("TripRoutePoints");
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

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.ClientPersonIdentity", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DAL.Entities.PersonIdentity.DriverPersonIdentity", b =>
                {
                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}