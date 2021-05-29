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

		public DbSet<Country> Countries { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Number> Numbers { get; set; }
		public DbSet<Registration> Registrations { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<EntityReservation> EntityReservations { get; set; }
		public DbSet<Bed> Beds { get; set; }
		public DbSet<NumberBed> NumberBeds { get; set; }
		public DbSet<NumberEntity> NumberEntities { get; set; }
		public DbSet<NumberType> NumberTypes { get; set; }
		public DbSet<NumberService> NumberServices { get; set; }
		public DbSet<Apartment> Apartments { get; set; }
		public DbSet<ApartmentService> ApartmentServices { get; set; }
		public DbSet<ApartmentType> ApartmentTypes { get; set; }
		public DbSet<ApartmentService> ApartmetServices { get; set; }
		public DbSet<Pack> Packs { get; set; }
		public DbSet<PackService> PackServices { get; set; }
		public DbSet<PackTenant> PackTenants { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Score> Scores { get; set; }
		public DbSet<ReviewScore> ReviewScores { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
/*
			builder.Entity<Country>().HasData(new[] { new Country { Code = "DC" } });*/

			builder.Entity<Country>().HasData(new Country() { Id = 1, Blocked = false, Name = "Россия"});
            builder.Entity<City>().HasData(new City() { Id = 1, Blocked = false, Name = "Москва", CountryId = 1});

            builder.Entity<ApartmentType>().HasData(new ApartmentType() { Id = 1, Description = "Отель со множеством комнат и номеров", Name = "Отель" });
            builder.Entity<ApartmentType>().HasData(new ApartmentType() { Id = 2, Description = "Частный дом обчно сожержит несколько квартир", Name = "Дом" });

            builder.Entity<NumberType>().HasData(new NumberType() { Id = 1, Name = "Одноместный номер", Share = false});
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 2, Name = "Двухместный номер", Share = false });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 3, Name = "Общая комната", Share = true });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 4, Name = "Апартаменты", Share = true });

            builder.Entity<Bed>().HasData(new Bed() { Id = 1, Name = "Односпальная", Code="SNG"});
            builder.Entity<Bed>().HasData(new Bed() { Id = 2, Name = "Двуспальная", Code="DBL"});

        }
    }
}
