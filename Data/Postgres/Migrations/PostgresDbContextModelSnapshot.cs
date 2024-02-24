﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using dotnet_mongodb.Data.Postgres;

#nullable disable

namespace dotnet_mongodb.Data.Postgres.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExpenseTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ExpenseId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("TagId");

                    b.ToTable("ExpenseTag");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.CreditCard.CreditCardEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserEmail");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.Expense.ExpenseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreditCardId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.Tag.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserEmail");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.User.UserEntity", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExpenseTag", b =>
                {
                    b.HasOne("dotnet_mongodb.Application.Expense.ExpenseEntity", null)
                        .WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dotnet_mongodb.Application.Tag.TagEntity", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dotnet_mongodb.Application.CreditCard.CreditCardEntity", b =>
                {
                    b.HasOne("dotnet_mongodb.Application.User.UserEntity", "User")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.Expense.ExpenseEntity", b =>
                {
                    b.HasOne("dotnet_mongodb.Application.CreditCard.CreditCardEntity", "CreditCard")
                        .WithMany("Expenses")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.Tag.TagEntity", b =>
                {
                    b.HasOne("dotnet_mongodb.Application.User.UserEntity", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.CreditCard.CreditCardEntity", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("dotnet_mongodb.Application.User.UserEntity", b =>
                {
                    b.Navigation("CreditCards");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
