﻿// <auto-generated />
using System;
using JamPlace.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JamPlace.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200511011021_ChangedEquipmentRelations")]
    partial class ChangedEquipmentRelations
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

            modelBuilder.Entity("JamPlace.DataLayer.Entities.CommentDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

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

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("EventEquipment");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.JamEventDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessType")
                        .HasColumnType("integer");

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("EventAdressId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Size")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventAdressId");

                    b.ToTable("JamEvents");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.JamEventJamUserDo", b =>
                {
                    b.Property<int>("JamEventDoId")
                        .HasColumnType("integer");

                    b.Property<int>("JamUserDoId")
                        .HasColumnType("integer");

                    b.Property<int>("AccessMode")
                        .HasColumnType("integer");

                    b.HasKey("JamEventDoId", "JamUserDoId");

                    b.HasIndex("JamUserDoId");

                    b.ToTable("JamEventJamUserDo");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.JamUserDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("PhotoBase64")
                        .HasColumnType("text");

                    b.Property<string>("UserIdentityId")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("JamUsers");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.NeededEquipmentDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("NeededEventEquipment");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.SongDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Artist")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.CommentDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.JamEventDo", "Event")
                        .WithMany("CommentsDo")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JamPlace.DataLayer.Entities.JamUserDo", "User")
                        .WithMany("CommentsDo")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.EquipmentDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.JamEventDo", "Event")
                        .WithMany("ProvidedEquipmentDos")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JamPlace.DataLayer.Entities.JamUserDo", "User")
                        .WithMany("ProvidedEquipmentDos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.JamEventDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.AdressDo", "EventAdress")
                        .WithMany("JamEvents")
                        .HasForeignKey("EventAdressId");
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.JamEventJamUserDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.JamEventDo", "JamEvent")
                        .WithMany("JamEventJamUser")
                        .HasForeignKey("JamEventDoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JamPlace.DataLayer.Entities.JamUserDo", "JamUser")
                        .WithMany("JamEventJamUser")
                        .HasForeignKey("JamUserDoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.NeededEquipmentDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.JamEventDo", "Event")
                        .WithMany("NeededEquipmentDos")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JamPlace.DataLayer.Entities.JamUserDo", "User")
                        .WithMany("NeededEquipmentDos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JamPlace.DataLayer.Entities.SongDo", b =>
                {
                    b.HasOne("JamPlace.DataLayer.Entities.JamEventDo", "Event")
                        .WithMany("SongsDo")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
