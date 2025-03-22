﻿// <auto-generated />
using System;
using Bookstore.Sample.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookstore.Sample.Migrations
{
    [DbContext(typeof(BookstoreDbContext))]
    partial class BookstoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.18");

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("ActionType")
                        .HasColumnType("tinyint");

                    b.Property<int>("EntityId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("EntityType")
                        .HasColumnType("smallint");

                    b.Property<int>("EventLogEntryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("EventLogEntryId");

                    b.ToTable("EntityLog", "eventlog");
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

                    b.Property<short>("EventType")
                        .HasColumnType("smallint");

                    b.Property<string>("FailureDetails")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.ToTable("EventLog", "eventlog");
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

                    b.Property<short>("PropertyType")
                        .HasColumnType("smallint");

                    b.Property<bool?>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.HasIndex("PropertyType");

                    b.ToTable("BoolPropertyLog", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DateTimePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("PropertyType")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.HasIndex("PropertyType");

                    b.ToTable("DateTimePropertyLog", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DecimalPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("PropertyType")
                        .HasColumnType("smallint");

                    b.Property<decimal?>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.HasIndex("PropertyType");

                    b.ToTable("DecimalPropertyLog", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DoublePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("PropertyType")
                        .HasColumnType("smallint");

                    b.Property<double?>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.HasIndex("PropertyType");

                    b.ToTable("DoublePropertyLog", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.Int32PropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("PropertyType")
                        .HasColumnType("smallint");

                    b.Property<int?>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.HasIndex("PropertyType");

                    b.ToTable("Int32PropertyLog", "eventlog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.StringPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("PropertyType")
                        .HasColumnType("smallint");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.HasIndex("PropertyType");

                    b.ToTable("StringPropertyLog", "eventlog");
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

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Bookstore.Sample.Models.ShelfEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Height")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EventLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EventLogEntry")
                        .WithMany("EntityLog")
                        .HasForeignKey("EventLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EventLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.BoolPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("BoolPropertyLog")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DateTimePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("DateTimePropertyLog")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DecimalPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("DecimalPropertyLog")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.DoublePropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("DoublePropertyLog")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.Int32PropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("Int32PropertyLog")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.PropertyLogEntries.StringPropertyLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.HasOne("AHSW.EventLog.Models.Entities.EntityLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", "EntityLogEntry")
                        .WithMany("StringPropertyLog")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
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
                    b.Navigation("BoolPropertyLog");

                    b.Navigation("DateTimePropertyLog");

                    b.Navigation("DecimalPropertyLog");

                    b.Navigation("DoublePropertyLog");

                    b.Navigation("Int32PropertyLog");

                    b.Navigation("StringPropertyLog");
                });

            modelBuilder.Entity("AHSW.EventLog.Models.Entities.EventLogEntry<Bookstore.Sample.Configurations.EventType, Bookstore.Sample.Configurations.EntityType, Bookstore.Sample.Configurations.PropertyType>", b =>
                {
                    b.Navigation("EntityLog");
                });

            modelBuilder.Entity("Bookstore.Sample.Models.ShelfEntity", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
