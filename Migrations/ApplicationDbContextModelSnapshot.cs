﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using theFoodCampus.Data;

#nullable disable

namespace theFoodCampus.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("theFoodCampus.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<string>("CommentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("theFoodCampus.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("theFoodCampus.Models.Instruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ReceipeId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesId")
                        .HasColumnType("int");

                    b.Property<string>("Step")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecipesId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("theFoodCampus.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RBudget")
                        .HasColumnType("int");

                    b.Property<int>("RDiet")
                        .HasColumnType("int");

                    b.Property<int>("REvent")
                        .HasColumnType("int");

                    b.Property<int>("RHoliday")
                        .HasColumnType("int");

                    b.Property<int>("RKashrut")
                        .HasColumnType("int");

                    b.Property<int>("RPrepTime")
                        .HasColumnType("int");

                    b.Property<int>("RType")
                        .HasColumnType("int");

                    b.Property<int>("RWeather")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("Rdifficulty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("theFoodCampus.Models.Comment", b =>
                {
                    b.HasOne("theFoodCampus.Models.Recipe", "Recipes")
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("theFoodCampus.Models.Ingredient", b =>
                {
                    b.HasOne("theFoodCampus.Models.Recipe", "Recipes")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("theFoodCampus.Models.Instruction", b =>
                {
                    b.HasOne("theFoodCampus.Models.Recipe", "Recipes")
                        .WithMany("Instructions")
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("theFoodCampus.Models.Recipe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ingredients");

                    b.Navigation("Instructions");
                });
#pragma warning restore 612, 618
        }
    }
}
