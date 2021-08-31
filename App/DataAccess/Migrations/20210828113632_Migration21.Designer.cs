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
    [Migration("20210828113632_Migration21")]
    partial class Migration21
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

                    b.Property<int?>("TerritoryID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MapID");

                    b.HasIndex("TerritoryID");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("DataAccess.Models.Continent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MapID");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("DataAccess.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<int>("Stage")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MapID");

                    b.ToTable("Games");
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

                    b.Property<string>("Continent1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Continent2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Continent3")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MapID")
                        .HasColumnType("int");

                    b.Property<int>("MissionType")
                        .HasColumnType("int");

                    b.Property<int>("NumTerritories")
                        .HasColumnType("int");

                    b.Property<string>("TargetPlayerColor")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("AvailableArmies")
                        .HasColumnType("int");

                    b.Property<bool>("Creator")
                        .HasColumnType("bit");

                    b.Property<int?>("GameID")
                        .HasColumnType("int");

                    b.Property<int?>("MissionID")
                        .HasColumnType("int");

                    b.Property<int>("OnTurn")
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

            modelBuilder.Entity("DataAccess.Models.PlayerCard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardID")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CardID");

                    b.HasIndex("PlayerID");

                    b.ToTable("PlayerCards");
                });

            modelBuilder.Entity("DataAccess.Models.PlayerColor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PlayerColors");
                });

            modelBuilder.Entity("DataAccess.Models.PlayerTerritory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Armies")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int?>("TerritoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TerritoryID");

                    b.ToTable("PlayerTerritories");
                });

            modelBuilder.Entity("DataAccess.Models.Territory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContinentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ContinentID");

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
                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapID");

                    b.HasOne("DataAccess.Models.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryID");
                });

            modelBuilder.Entity("DataAccess.Models.Continent", b =>
                {
                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapID");
                });

            modelBuilder.Entity("DataAccess.Models.Game", b =>
                {
                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapID");
                });

            modelBuilder.Entity("DataAccess.Models.Mission", b =>
                {
                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany()
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
                        .WithMany()
                        .HasForeignKey("GameID");

                    b.HasOne("DataAccess.Models.Mission", "Mission")
                        .WithMany()
                        .HasForeignKey("MissionID");

                    b.HasOne("DataAccess.Models.PlayerColor", "PlayerColor")
                        .WithMany()
                        .HasForeignKey("PlayerColorID");

                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("DataAccess.Models.PlayerCard", b =>
                {
                    b.HasOne("DataAccess.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardID");

                    b.HasOne("DataAccess.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID");
                });

            modelBuilder.Entity("DataAccess.Models.PlayerTerritory", b =>
                {
                    b.HasOne("DataAccess.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID");

                    b.HasOne("DataAccess.Models.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryID");
                });

            modelBuilder.Entity("DataAccess.Models.Territory", b =>
                {
                    b.HasOne("DataAccess.Models.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentID");
                });
#pragma warning restore 612, 618
        }
    }
}
