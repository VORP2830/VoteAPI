﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VoteAPI.Infra.Data.Context;

#nullable disable

namespace VoteAPI.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VoteAPI.Domain.Entities.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("VoteAPI.Domain.Entities.Schedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FinishingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("VoteAPI.Domain.Entities.Vote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<long>("ScheduleId")
                        .HasColumnType("bigint");

                    b.Property<string>("VoteOption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("VoteAPI.Domain.Entities.Vote", b =>
                {
                    b.HasOne("VoteAPI.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VoteAPI.Domain.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Schedule");
                });
#pragma warning restore 612, 618
        }
    }
}
