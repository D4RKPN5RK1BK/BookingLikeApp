﻿using BookingLikeApp.Models;
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
		public DbSet<Street> Streets { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Number> Numbers { get; set; }
		public DbSet<Registration> Registrations { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<EntityReservation> EntityReservations { get; set; }
		public DbSet<Bed> Beds { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<NumberBed> NumberBeds { get; set; }
		public DbSet<NumberEntity> NumberEntities { get; set; }
		public DbSet<NumberRoom> NumberRooms { get; set; }
		public DbSet<NumberType> NumberTypes { get; set; }
		public DbSet<NumberRoomBed> NumberRoomBeds { get; set; }
		public DbSet<NumberService> NumberServices { get; set; }
		public DbSet<Apartment> Apartments { get; set; }
		public DbSet<ApartmentCard> ApartmentCards { get; set; }
		public DbSet<ApartmentService> ApartmentServices { get; set; }
		public DbSet<ApartmentType> ApartmentTypes { get; set; }
		public DbSet<ApartmentService> ApartmetServices { get; set; }
		public DbSet<Pack> Packs { get; set; }
		public DbSet<PackService> PackServices { get; set; }
		public DbSet<PackTenant> PackTenants { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Score> Scores { get; set; }
		public DbSet<ReviewScore> ReviewScores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(new Country() { Id = 1, Blocked = false, Name = "Россия"});
            builder.Entity<City>().HasData(new City() { Id = 1, Blocked = false, Name = "Москва", CountryId = 1});
            builder.Entity<Street>().HasData(new Street() { Id = 1, Name = "Ленинградская", CityId = 1});

            builder.Entity<ApartmentType>().HasData(new ApartmentType() { Id = 1, Description = "Отель со множеством комнат и номеров", Name = "Отель" });
            builder.Entity<ApartmentType>().HasData(new ApartmentType() { Id = 2, Description = "Частный дом обчно сожержит несколько квартир", Name = "Дом" });

            builder.Entity<NumberType>().HasData(new NumberType() { Id = 1, Name = "Одноместный номер", HasRooms = false, Share = false});
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 2, Name = "Двухместный номер", HasRooms = true, Share = false });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 3, Name = "Общая комната", HasRooms = false, Share = true });
            builder.Entity<NumberType>().HasData(new NumberType() { Id = 4, Name = "Апартаменты", HasRooms = true, Share = true });

            builder.Entity<Room>().HasData(new Room() { Id = 1, Name = "Спальная"});
            builder.Entity<Room>().HasData(new Room() { Id = 2, Name = "Гостинная"});
            builder.Entity<Room>().HasData(new Room() { Id = 3, Name = "Ванная"});

            builder.Entity<Bed>().HasData(new Bed() { Id = 1, Name = "Односпальная", Code="SNG"});
            builder.Entity<Bed>().HasData(new Bed() { Id = 2, Name = "Двуспальная", Code="DBL"});

            builder.Entity<Card>().HasData(new Card() { Id = 1, Name = "MasterCard" });
            builder.Entity<Card>().HasData(new Card() { Id = 2, Name = "ВТБ" });
        }
    }
}
