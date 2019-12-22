﻿// <auto-generated />
using JamPlace.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JamPlace.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191221171957_AddedRelationPersonalEquipmentUser")]
    partial class AddedRelationPersonalEquipmentUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("JamPlace.DataLayer.Entities.AdressDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("LocalNumber")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.BasicJamEventDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Size")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BasicJamEvents");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.CommentDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.EquipmentDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.EventEquipmentUserDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EquipmentId")
                        .HasColumnType("integer");

                    b.Property<int>("JamEventId")
                        .HasColumnType("integer");

                    b.Property<int>("JamUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("EquipmentUser");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.JamUserDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("UserIdentityId")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("JamUsers");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.NeededEquipmentEventDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EquipmentId")
                        .HasColumnType("integer");

                    b.Property<int>("JamEventId")
                        .HasColumnType("integer");

                    b.Property<int>("Quanity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("NeededEquipmentEvent");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.PersonalEquipmentUserDo", b =>
                {
                    b.Property<int>("JamUserDoId")
                        .HasColumnType("integer");

                    b.Property<int>("EquipmentDoId")
                        .HasColumnType("integer");

                    b.HasKey("JamUserDoId", "EquipmentDoId");

                    b.HasIndex("EquipmentDoId");

                    b.ToTable("PersonalEquipmentUserDo");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.SongDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Artist")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.PersonalEquipmentUserDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.EquipmentDo", "Equipment")
                        .WithMany("OwningUsers")
                        .HasForeignKey("EquipmentDoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JamPlace.DataLayer.Entities.JamUserDo", "JamUser")
                        .WithMany("UserPersonalEquipment")
                        .HasForeignKey("JamUserDoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
