﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using matrix_task.Helpers;

namespace matrix_task.Migrations
{
    [DbContext(typeof(HeroDBcontext))]
    [Migration("20200829231716_Hero")]
    partial class Hero
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("matrix_task.Entities.Ability", b =>
                {
                    b.Property<int>("AbilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("AbilityId");

                    b.ToTable("Ability");
                });

            modelBuilder.Entity("matrix_task.Entities.Hero", b =>
                {
                    b.Property<int>("HeroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AbilityId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CurrentPower")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("StartingPower")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Suit")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("HeroId");

                    b.HasIndex("AbilityId");

                    b.ToTable("Hero");
                });

            modelBuilder.Entity("matrix_task.Entities.Trainer", b =>
                {
                    b.Property<int>("TrainerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BINARY(64)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BINARY(128)");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("TrainerId");

                    b.ToTable("Trainer");
                });

            modelBuilder.Entity("matrix_task.Entities.TrainerHero", b =>
                {
                    b.Property<int>("TrainerHeroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HeroId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TrainerHeroId");

                    b.HasIndex("HeroId");

                    b.HasIndex("TrainerId");

                    b.ToTable("TrainerHero");
                });

            modelBuilder.Entity("matrix_task.Entities.Hero", b =>
                {
                    b.HasOne("matrix_task.Entities.Ability", "Ability")
                        .WithMany("Heroes")
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("matrix_task.Entities.TrainerHero", b =>
                {
                    b.HasOne("matrix_task.Entities.Hero", "Hero")
                        .WithMany("TrainerHeroes")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("matrix_task.Entities.Trainer", "Trainer")
                        .WithMany("TrainerHeroes")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}