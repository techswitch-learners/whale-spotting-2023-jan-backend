﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WhaleSpotting;

#nullable disable

namespace WhaleSpotting.Migrations
{
    [DbContext(typeof(WhaleSpottingDbContext))]
    [Migration("20230323150939_updateDB")]
    partial class updateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WhaleSpotting.Models.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("UserBio")
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Database.WhaleSighting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApprovalStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfSighting")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<float>("LocationLatitude")
                        .HasColumnType("real");

                    b.Property<float>("LocationLongitude")
                        .HasColumnType("real");

                    b.Property<int>("NumberOfWhales")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoImageURL")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WhaleSpeciesId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WhaleSpeciesId");

                    b.ToTable("WhaleSightings");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Database.WhaleSpecies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Colour")
                        .HasColumnType("text");

                    b.Property<string>("Diet")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("TailType")
                        .HasColumnType("integer");

                    b.Property<int>("TeethType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WhaleSpecies");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Database.WhaleSighting", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("WhaleSpotting.Models.Database.WhaleSpecies", "WhaleSpecies")
                        .WithMany()
                        .HasForeignKey("WhaleSpeciesId");

                    b.Navigation("User");

                    b.Navigation("WhaleSpecies");
                });
#pragma warning restore 612, 618
        }
    }
}
