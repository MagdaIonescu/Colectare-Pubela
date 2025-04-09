﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using colectare_date.Data;

#nullable disable

namespace colectare_date.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250402195229_AdaugareLongitudineSiLatitudineInTabelaColectari")]
    partial class AdaugareLongitudineSiLatitudineInTabelaColectari
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("colectare_date.Models.Cetatean", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CNP")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenume")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cetateni");
                });

            modelBuilder.Entity("colectare_date.Models.Colectare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Latitudine")
                        .HasColumnType("REAL");

                    b.Property<float>("Longitudine")
                        .HasColumnType("REAL");

                    b.Property<string>("PubelaId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimpColectare")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Colectari");
                });

            modelBuilder.Entity("colectare_date.Models.Pubela", b =>
                {
                    b.Property<string>("PubelaId")
                        .HasColumnType("TEXT");

                    b.HasKey("PubelaId");

                    b.ToTable("Pubele");
                });

            modelBuilder.Entity("colectare_date.Models.PubelaCetateni", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CetateanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PubelaId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CetateanId");

                    b.HasIndex("PubelaId");

                    b.ToTable("PubeleCetateni");
                });

            modelBuilder.Entity("colectare_date.Models.Utilizator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Parola")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Utilizatori");
                });

            modelBuilder.Entity("colectare_date.Models.PubelaCetateni", b =>
                {
                    b.HasOne("colectare_date.Models.Cetatean", "Cetatean")
                        .WithMany()
                        .HasForeignKey("CetateanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("colectare_date.Models.Pubela", "Pubela")
                        .WithMany()
                        .HasForeignKey("PubelaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cetatean");

                    b.Navigation("Pubela");
                });
#pragma warning restore 612, 618
        }
    }
}
