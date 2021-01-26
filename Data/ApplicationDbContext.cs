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
        DbSet<Apartment> Apartments { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Street> Streets { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Number> Numbers { get; set; }
        /*DbSet<NumberName> NumberNames { get; set; }*/
        DbSet<NumberType> NumberTypes { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<ApartmentType> ApartmentTypes { get; set; }
        DbSet<Bed> Beds { get; set; }
        /*DbSet<NumberBed> NumberBeds { get; set; }
        DbSet<ReservationBed> ReservationBeds { get; set; }*/

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(new Country() { Id = 1, Blocked = false, Name = "TestCountry"});
            builder.Entity<City>().HasData(new City() { Id = 1, Blocked = false, Name = "TestCity", CountryId = 1});
            builder.Entity<Street>().HasData(new Street() { Id = 1, Name = "TestCountry", CityId = 1});

            /*builder.Entity<Rating>().HasNoKey();*/

            /*builder.Entity<ApartmentType>().HasData(
                new ApartmentType() { Id = 1, Name = "Home", NormalizedName="HOME", Single = true},
                new ApartmentType() { Id = 2, Name = "Other", NormalizedName = "OTHER", Single = false},    
                new ApartmentType() { Id = 3, Name = "Hotel", NormalizedName = "HOTEL", Single = false},    
                new ApartmentType() { Id = 4, Name = "Apartment", NormalizedName = "APARTMENT", Single = false}   
            );*/

        }
    }
}
