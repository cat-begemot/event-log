﻿// <auto-generated />
using System;
using Bookstore.Sample.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookstore.Sample.Migrations
{
    [DbContext(typeof(BookstoreDbContext))]
    [Migration("20250225133907_UpdateBookTableByLabelsProperty")]
    partial class UpdateBookTableByLabelsProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.18");

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActionType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventLogEntryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventLogEntryId");

                    b.ToTable("EntityLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EntityTypeDescription", b =>
                {
                    b.Property<int>("EnumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("EnumId");

                    b.ToTable("EntityTypeDescriptions", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EventLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CompletedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FailureDetails")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("EventLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EventStatusDescription", b =>
                {
                    b.Property<int>("EnumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("EnumId");

                    b.ToTable("EventStatusDescriptions", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EventTypeDescription", b =>
                {
                    b.Property<int>("EnumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("EnumId");

                    b.ToTable("EventTypeDescriptions", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.BoolPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("BoolPropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DateTimePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("DateTimePropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DecimalPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("DecimalPropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DoublePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("DoublePropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.Int32PropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("Int32PropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.StringPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("StringPropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyTypeDescription", b =>
                {
                    b.Property<int>("EnumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("EnumId");

                    b.ToTable("PropertyTypeDescriptions", "eventlog");
                });

            modelBuilder.Entity("Bookstore.Sample.Models.BookEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Condition")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FirstSale")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Labels")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LikeCount")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Price")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Published")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ShelfId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShelfId");

                    b.ToTable("ApplicationEntities");
                });

            modelBuilder.Entity("Bookstore.Sample.Models.ShelfEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Height")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ApplicationOtherEntities");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EventLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EventLogEntry")
                        .WithMany("EntityLogEntries")
                        .HasForeignKey("EventLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.BoolPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("BoolPropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DateTimePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("DateTimePropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DecimalPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("DecimalPropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DoublePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("DoublePropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.Int32PropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("Int32PropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.StringPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("StringPropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("Bookstore.Sample.Models.BookEntity", b =>
                {
                    b.HasOne("Bookstore.Sample.Models.ShelfEntity", "Shelf")
                        .WithMany("Books")
                        .HasForeignKey("ShelfId");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Navigation("BoolPropertyLogEntries");

                    b.Navigation("DateTimePropertyLogEntries");

                    b.Navigation("DecimalPropertyLogEntries");

                    b.Navigation("DoublePropertyLogEntries");

                    b.Navigation("Int32PropertyLogEntries");

                    b.Navigation("StringPropertyLogEntries");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EventLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Navigation("EntityLogEntries");
                });

            modelBuilder.Entity("Bookstore.Sample.Models.ShelfEntity", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
