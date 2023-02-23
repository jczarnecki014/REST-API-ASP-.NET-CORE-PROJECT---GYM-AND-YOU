﻿using GymAndYou.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymAndYou.DatabaseConnection;

    public class DbConnection:DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options):base(options)
        {

        }

        public DbSet<Gym> Gyms {get;set;}
        public DbSet<Address> Addreses {get;set;}
        public DbSet<Members> Members {get;set;}
        public DbSet<AviableEquipment> AviableEquipments {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Gym>()
                .Property(u=>u.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Members>()
                .Property(u=>u.Email)
                .IsRequired();
            modelBuilder.Entity<Members>()
                .Property(u=>u.Pesel)
                .IsRequired();
            modelBuilder.Entity<AviableEquipment>()
                .Property(u=>u.Name)
                .IsRequired();
            modelBuilder.Entity<AviableEquipment>()
                .Property(u=>u.BodyPart)
                .IsRequired();
        }
    }

