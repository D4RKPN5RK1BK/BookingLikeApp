﻿// <auto-generated />
using System;
using BookingLikeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookingLikeApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210202035917_Enable")]
    partial class Enable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BookingLikeApp.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("AccidentProtection")
                        .HasColumnType("bit");

                    b.Property<string>("AdditionalPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("AnimalsAllowed")
                        .HasColumnType("bit");

                    b.Property<int>("ApartmentStreetId")
                        .HasColumnType("int");

                    b.Property<int>("ApartmentTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalTimeEnds")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ArrivalTimeStarts")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Bar")
                        .HasColumnType("bit");

                    b.Property<int>("Breakfest")
                        .HasColumnType("int");

                    b.Property<bool>("CancelPrice")
                        .HasColumnType("bit");

                    b.Property<bool>("Checked")
                        .HasColumnType("bit");

                    b.Property<bool>("ChildrensAllowed")
                        .HasColumnType("bit");

                    b.Property<string>("ContactPerson")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DaysUntilCancelEnds")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTimeEnds")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTimeStarts")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Disabled")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.Property<bool>("FamilyNumbers")
                        .HasColumnType("bit");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<bool>("Fitnes")
                        .HasColumnType("bit");

                    b.Property<bool>("FreeWiFi")
                        .HasColumnType("bit");

                    b.Property<bool>("FullTimeRegistration")
                        .HasColumnType("bit");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Parking")
                        .HasColumnType("int");

                    b.Property<int>("PolisherStreetId")
                        .HasColumnType("int");

                    b.Property<bool>("Pool")
                        .HasColumnType("bit");

                    b.Property<string>("SecondAddressLine")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("SmokeFreeNumbers")
                        .HasColumnType("bit");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<int?>("StreetId")
                        .HasColumnType("int");

                    b.Property<bool>("UseCards")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentTypeId");

                    b.HasIndex("StreetId");

                    b.HasIndex("UserId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("BookingLikeApp.Models.ApartmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApartmentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "TestType",
                            Name = "TestType"
                        });
                });

            modelBuilder.Entity("BookingLikeApp.Models.Bed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Capacity")
                        .HasColumnType("decimal(1,0)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Beds");
                });

            modelBuilder.Entity("BookingLikeApp.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Blocked = false,
                            CountryId = 1,
                            Name = "TestCity"
                        });
                });

            modelBuilder.Entity("BookingLikeApp.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<string>("Code")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("FlagUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Blocked = false,
                            Name = "TestCountry"
                        });
                });

            modelBuilder.Entity("BookingLikeApp.Models.Number", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("AllowSmoke")
                        .HasColumnType("bit");

                    b.Property<int>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<int>("Bathrooms")
                        .HasColumnType("int");

                    b.Property<int>("BedId")
                        .HasColumnType("int");

                    b.Property<int>("BedQuantity")
                        .HasColumnType("int");

                    b.Property<int>("Bedrooms")
                        .HasColumnType("int");

                    b.Property<bool>("CityView")
                        .HasColumnType("bit");

                    b.Property<string>("CustomName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset>("Disabled")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool>("FreeWiFi")
                        .HasColumnType("bit");

                    b.Property<int>("LinvingRooms")
                        .HasColumnType("int");

                    b.Property<bool>("MiniBar")
                        .HasColumnType("bit");

                    b.Property<int>("NumberTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("OceanView")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(12,2)");

                    b.Property<bool>("ProvidedTV")
                        .HasColumnType("bit");

                    b.Property<bool>("Shared")
                        .HasColumnType("bit");

                    b.Property<bool>("SoundIsolation")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("NumberTypeId");

                    b.ToTable("Numbers");
                });

            modelBuilder.Entity("BookingLikeApp.Models.NumberBed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BedId")
                        .HasColumnType("int");

                    b.Property<int>("NumberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BedId");

                    b.HasIndex("NumberId");

                    b.ToTable("NumberBed");
                });

            modelBuilder.Entity("BookingLikeApp.Models.NumberReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("NumberId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("ReservationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NumberId");

                    b.HasIndex("ReservationId");

                    b.ToTable("NumberReservation");
                });

            modelBuilder.Entity("BookingLikeApp.Models.NumberType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LongName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Id");

                    b.ToTable("NumberTypes");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Registration", b =>
                {
                    b.Property<int>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("BasicInfo")
                        .HasColumnType("bit");

                    b.Property<bool>("Facilites")
                        .HasColumnType("bit");

                    b.Property<bool>("FacilitesRequired")
                        .HasColumnType("bit");

                    b.Property<bool>("Numbers")
                        .HasColumnType("bit");

                    b.Property<bool>("Payment")
                        .HasColumnType("bit");

                    b.Property<bool>("Photos")
                        .HasColumnType("bit");

                    b.Property<bool>("Rules")
                        .HasColumnType("bit");

                    b.Property<bool>("Services")
                        .HasColumnType("bit");

                    b.HasKey("ApartmentId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AbortCancel")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(2,0)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(12,2)");

                    b.Property<DateTime>("ProceedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Streets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Name = "TestCountry"
                        });
                });

            modelBuilder.Entity("BookingLikeApp.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Apartment", b =>
                {
                    b.HasOne("BookingLikeApp.Models.ApartmentType", "ApartmentType")
                        .WithMany("Apartments")
                        .HasForeignKey("ApartmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingLikeApp.Models.Street", null)
                        .WithMany("Apartments")
                        .HasForeignKey("StreetId");

                    b.HasOne("BookingLikeApp.Models.User", "User")
                        .WithMany("Apartments")
                        .HasForeignKey("UserId");

                    b.Navigation("ApartmentType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingLikeApp.Models.City", b =>
                {
                    b.HasOne("BookingLikeApp.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Number", b =>
                {
                    b.HasOne("BookingLikeApp.Models.Apartment", "Apartment")
                        .WithMany("Numbers")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingLikeApp.Models.NumberType", "NumberType")
                        .WithMany()
                        .HasForeignKey("NumberTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");

                    b.Navigation("NumberType");
                });

            modelBuilder.Entity("BookingLikeApp.Models.NumberBed", b =>
                {
                    b.HasOne("BookingLikeApp.Models.Bed", "Bed")
                        .WithMany("NumberBeds")
                        .HasForeignKey("BedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingLikeApp.Models.Number", "Number")
                        .WithMany("NumberBeds")
                        .HasForeignKey("NumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bed");

                    b.Navigation("Number");
                });

            modelBuilder.Entity("BookingLikeApp.Models.NumberReservation", b =>
                {
                    b.HasOne("BookingLikeApp.Models.Number", "Number")
                        .WithMany("NumberReservations")
                        .HasForeignKey("NumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingLikeApp.Models.Reservation", "Reservation")
                        .WithMany("NumberReservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Number");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Photo", b =>
                {
                    b.HasOne("BookingLikeApp.Models.Apartment", "Apartment")
                        .WithMany("Photos")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Registration", b =>
                {
                    b.HasOne("BookingLikeApp.Models.Apartment", "Apartment")
                        .WithOne("Registration")
                        .HasForeignKey("BookingLikeApp.Models.Registration", "ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Reservation", b =>
                {
                    b.HasOne("BookingLikeApp.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Street", b =>
                {
                    b.HasOne("BookingLikeApp.Models.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookingLikeApp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookingLikeApp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingLikeApp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookingLikeApp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookingLikeApp.Models.Apartment", b =>
                {
                    b.Navigation("Numbers");

                    b.Navigation("Photos");

                    b.Navigation("Registration");
                });

            modelBuilder.Entity("BookingLikeApp.Models.ApartmentType", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Bed", b =>
                {
                    b.Navigation("NumberBeds");
                });

            modelBuilder.Entity("BookingLikeApp.Models.City", b =>
                {
                    b.Navigation("Streets");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Number", b =>
                {
                    b.Navigation("NumberBeds");

                    b.Navigation("NumberReservations");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Reservation", b =>
                {
                    b.Navigation("NumberReservations");
                });

            modelBuilder.Entity("BookingLikeApp.Models.Street", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("BookingLikeApp.Models.User", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
