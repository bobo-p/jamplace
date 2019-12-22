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
        public DbSet<BasicJamEventDo> BasicJamEvents { get; set; }
        public DbSet<CommentDo> Comments { get; set; }
        public DbSet<EquipmentDo> Equipment { get; set; }
        public DbSet<UserEventEquipmentDo> EventEquipment{ get; set; }
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

            modelBuilder.Entity<JamUserEventEquipmentDo>()
               .HasKey(bc => new { bc.JamUserDoId, bc.EventEquipmentDoId });
            modelBuilder.Entity<JamUserEventEquipmentDo>()
                .HasOne(bc => bc.JamUser)
                .WithMany(b => b.UserEventEquipment)
                .HasForeignKey(bc => bc.JamUserDoId);
            modelBuilder.Entity<JamUserEventEquipmentDo>()
                .HasOne(bc => bc.EventEquipment)
                .WithMany(c => c.EventJamUsers)
                .HasForeignKey(bc => bc.EventEquipmentDoId);
        }
    }
}
