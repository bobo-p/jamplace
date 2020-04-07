using JamPlace.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<JamEventDo> JamEvents { get; set; }
        public DbSet<CommentDo> Comments { get; set; }
        public DbSet<EquipmentDo> Equipment { get; set; }
        public DbSet<EventEquipmentDo> EventEquipment{ get; set; }
        public DbSet<NeededEquipmentEventDo> NeededEquipmentEvent { get; set; }
        public DbSet<JamUserDo> JamUsers{ get; set; }
        public DbSet<AdressDo> Addresses { get; set;}
        public DbSet<SongDo> Songs{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalEquipmentUserDo>()
                .HasKey(bc => new { bc.JamUserDoId, bc.EquipmentDoId });
            modelBuilder.Entity<PersonalEquipmentUserDo>()
                .HasOne(bc => bc.JamUser)
                .WithMany(b => b.UserPersonalEquipment)
                .HasForeignKey(bc => bc.JamUserDoId);
            modelBuilder.Entity<PersonalEquipmentUserDo>()
                .HasOne(bc => bc.Equipment)
                .WithMany(c => c.OwningUsers)
                .HasForeignKey(bc => bc.EquipmentDoId);

            modelBuilder.Entity<EquipmentEventEquipmentDo>()
                .HasKey(bc => new { bc.EventEquipmentDoId, bc.EquipmentDoId });
            modelBuilder.Entity<EquipmentEventEquipmentDo>()
                .HasOne(bc => bc.EventEquipment)
                .WithMany(b => b.UserEventEventEqupiment)
                .HasForeignKey(bc => bc.EventEquipmentDoId);
            modelBuilder.Entity<EquipmentEventEquipmentDo>()
                .HasOne(bc => bc.Equipment)
                .WithMany(c => c.EquipmentEventEquipmentDos)
                .HasForeignKey(bc => bc.EquipmentDoId);

            modelBuilder.Entity<JamEventJamUserDo>()
               .HasKey(bc => new { bc.JamEventDoId, bc.JamUserDoId });
            modelBuilder.Entity<JamEventJamUserDo>()
                .HasOne(bc => bc.JamUser)
                .WithMany(b => b.JamEventJamUser)
                .HasForeignKey(bc => bc.JamUserDoId);
            modelBuilder.Entity<JamEventJamUserDo>()
                .HasOne(bc => bc.JamEvent)
                .WithMany(c => c.JamEventJamUser)
                .HasForeignKey(bc => bc.JamEventDoId);

            modelBuilder.Entity<NeededEquipmentEventDo>()
               .HasKey(bc => new { bc.JamEventDoId, bc.EquipmentDoId });
            modelBuilder.Entity<NeededEquipmentEventDo>()
                .HasOne(bc => bc.JamEvent)
                .WithMany(b => b.NeededEventEquipment)
                .HasForeignKey(bc => bc.JamEventDoId);
            modelBuilder.Entity<NeededEquipmentEventDo>()
                .HasOne(bc => bc.Equipment)
                .WithMany(c => c.NeededForEvents)
                .HasForeignKey(bc => bc.EquipmentDoId);

            //modelBuilder.Entity<JamEventDo>()
            //    .HasOne<AdressDo>(s => s.Adress)
            //    .WithMany(g => g.JamEvents)
            //    .HasForeignKey(s => s.AddressId);

        }
    }
}
