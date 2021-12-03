﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuoteQuiz.DataAccess.EntityFramework;

namespace QuoteQuiz.DataAccess.Migrations
{
    [DbContext(typeof(QuoteQuizDbContext))]
    partial class QuoteQuizDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.Answers_Binary", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CorrectAnswer")
                        .HasColumnType("bit");

                    b.Property<int>("QuoteID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("QuoteID")
                        .IsUnique();

                    b.ToTable("Answers_Binary");
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.Answers_Multiple", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("PossibleAnwerText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuoteID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("QuoteID");

                    b.ToTable("Answers_Multiple");
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.Quotes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<string>("QuoteText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.User_Answers", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuoteID")
                        .HasColumnType("int");

                    b.Property<bool?>("UserBinaryAnswer")
                        .HasColumnType("bit");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("UserMultipleAnswerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("QuoteID");

                    b.HasIndex("UserID");

                    b.ToTable("User_Answers");
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentMode")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.Answers_Binary", b =>
                {
                    b.HasOne("QuoteQuiz.DataAccess.Entities.Quotes", "Quote")
                        .WithOne("Answers_Binary")
                        .HasForeignKey("QuoteQuiz.DataAccess.Entities.Answers_Binary", "QuoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.Answers_Multiple", b =>
                {
                    b.HasOne("QuoteQuiz.DataAccess.Entities.Quotes", "Quote")
                        .WithMany("Answers_Multiple")
                        .HasForeignKey("QuoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuoteQuiz.DataAccess.Entities.User_Answers", b =>
                {
                    b.HasOne("QuoteQuiz.DataAccess.Entities.Quotes", "Quote")
                        .WithMany("User_Answers")
                        .HasForeignKey("QuoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuoteQuiz.DataAccess.Entities.Users", "User")
                        .WithMany("User_Answers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
