﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(RizzyCoContext))]
    [Migration("20210115074710_EighthVersion")]
    partial class EighthVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Models.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int?>("TerritoryID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MapID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TerritoryID");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("DataAccess.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatorID")
                        .HasColumnType("int");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatorID");

                    b.HasIndex("MapID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DataAccess.Models.GameUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.HasIndex("UserID");

                    b.ToTable("GamesUser");
                });

            modelBuilder.Entity("DataAccess.Models.Map", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfAvailableArmies")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfContinents")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTerritories")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("DataAccess.Models.Mission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MapID");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("DataAccess.Models.Neighbour", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DstID")
                        .HasColumnType("int");

                    b.Property<int?>("SrcID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DstID");

                    b.HasIndex("SrcID");

                    b.ToTable("Neighbours");
                });

            modelBuilder.Entity("DataAccess.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameID")
                        .HasColumnType("int");

                    b.Property<int?>("MissionID")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerColorID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.HasIndex("MissionID");

                    b.HasIndex("PlayerColorID");

                    b.HasIndex("UserID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DataAccess.Models.PlayerColor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int?>("GameID")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.ToTable("PlayerColors");
                });

            modelBuilder.Entity("DataAccess.Models.Territory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MapID");

                    b.HasIndex("PlayerID");

                    b.ToTable("Territories");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.Models.Card", b =>
                {
                    b.HasOne("DataAccess.Models.Map", null)
                        .WithMany("Cards")
                        .HasForeignKey("MapID");

                    b.HasOne("DataAccess.Models.Player", "Player")
                        .WithMany("Cards")
                        .HasForeignKey("PlayerID");

                    b.HasOne("DataAccess.Models.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryID");
                });

            modelBuilder.Entity("DataAccess.Models.Game", b =>
                {
                    b.HasOne("DataAccess.Models.User", "Creator")
                        .WithMany("Games")
                        .HasForeignKey("CreatorID");

                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapID");
                });

            modelBuilder.Entity("DataAccess.Models.GameUser", b =>
                {
                    b.HasOne("DataAccess.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameID");

                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("DataAccess.Models.Mission", b =>
                {
                    b.HasOne("DataAccess.Models.Map", null)
                        .WithMany("Missions")
                        .HasForeignKey("MapID");
                });

            modelBuilder.Entity("DataAccess.Models.Neighbour", b =>
                {
                    b.HasOne("DataAccess.Models.Territory", "Dst")
                        .WithMany()
                        .HasForeignKey("DstID");

                    b.HasOne("DataAccess.Models.Territory", "Src")
                        .WithMany()
                        .HasForeignKey("SrcID");
                });

            modelBuilder.Entity("DataAccess.Models.Player", b =>
                {
                    b.HasOne("DataAccess.Models.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameID");

                    b.HasOne("DataAccess.Models.Mission", "Mission")
                        .WithMany()
                        .HasForeignKey("MissionID");

                    b.HasOne("DataAccess.Models.PlayerColor", "PlayerColor")
                        .WithMany()
                        .HasForeignKey("PlayerColorID");

                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("DataAccess.Models.PlayerColor", b =>
                {
                    b.HasOne("DataAccess.Models.Game", null)
                        .WithMany("PlayerColors")
                        .HasForeignKey("GameID");
                });

            modelBuilder.Entity("DataAccess.Models.Territory", b =>
                {
                    b.HasOne("DataAccess.Models.Map", null)
                        .WithMany("Territories")
                        .HasForeignKey("MapID");

                    b.HasOne("DataAccess.Models.Player", "Player")
                        .WithMany("Territories")
                        .HasForeignKey("PlayerID");
                });
#pragma warning restore 612, 618
        }
    }
}
