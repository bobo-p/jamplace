using JamPlace.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<JamEventDo> JamEvents { get; set; }
        public DbSet<CommentDo> Comments { get; set; }
        public DbSet<EquipmentDo> Equipment { get; set; }
        public DbSet<EquipmentUserDo> EquipmentUser { get; set; }
        public DbSet<NeededEquipmentEventDo> NeededEquipmentEvent { get; set; }
        public DbSet<JamUserDo> JamUsers{ get; set; }
        public DbSet<AdressDo> Addresses { get; set;}
        public DbSet<SongDo> Songs{ get; set; }
    }
}
