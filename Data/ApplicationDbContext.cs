using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingLikeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Number> Numbers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<ApartmentCard> ApartmentCards { get; set; }
        public DbSet<NumberType> NumberTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<NumberBed> NumberBeds { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<NumberRoom> NumberRooms { get; set; }
        public DbSet<NumberRoomBed> NumberRoomBeds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(new Country() { Id = 1, Blocked = false, Name = "TestCountry"});
            builder.Entity<City>().HasData(new City() { Id = 1, Blocked = false, Name = "TestCity", CountryId = 1});
            builder.Entity<Street>().HasData(new Street() { Id = 1, Name = "TestCountry", CityId = 1});

            builder.Entity<ApartmentType>().HasData(new ApartmentType() { Id = 1, Description = "TestType", Name = "TestType" });

            builder.Entity<NumberType>().HasData(new NumberType() { Id = 1, Name = "Number", HasRooms = false, Share = false});
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 2, Name = "Apartment", HasRooms = true, Share = false });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 3, Name = "SharedRoom", HasRooms = false, Share = true });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 4, Name = "SharedBed", HasRooms = false, Share = true });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 5, Name = "SharedBedWithRooms", HasRooms = true, Share = true });

            builder.Entity<Room>().HasData(new Room() { Id = 1, Name = "Bedroom"});
            builder.Entity<Room>().HasData(new Room() { Id = 2, Name = "Livingroom"});
            builder.Entity<Room>().HasData(new Room() { Id = 3, Name = "Bathroom"});

            builder.Entity<Card>().HasData(new Card() { Id = 1, Name = "TestCard" });
        }
    }
}
