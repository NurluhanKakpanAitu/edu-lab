﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Domain.Entities.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("VideoPath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Domain.Entities.PracticeWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("PracticeWorks");
                });

            modelBuilder.Entity("Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Domain.Entities.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Answer", b =>
                {
                    b.HasOne("Domain.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.Base.Translation", "Title", b1 =>
                        {
                            b1.Property<Guid>("AnswerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text")
                                .HasColumnName("TitleEn");

                            b1.Property<string>("Kz")
                                .HasColumnType("text")
                                .HasColumnName("TitleKz");

                            b1.Property<string>("Ru")
                                .HasColumnType("text")
                                .HasColumnName("TitleRu");

                            b1.HasKey("AnswerId");

                            b1.ToTable("Answers");

                            b1.WithOwner()
                                .HasForeignKey("AnswerId");
                        });

                    b.Navigation("Question");

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.OwnsOne("Domain.Entities.Base.Translation", "Description", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text");

                            b1.Property<string>("Kz")
                                .HasColumnType("text");

                            b1.Property<string>("Ru")
                                .HasColumnType("text");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses");

                            b1.ToJson("Description");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.OwnsOne("Domain.Entities.Base.Translation", "Title", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text");

                            b1.Property<string>("Kz")
                                .HasColumnType("text");

                            b1.Property<string>("Ru")
                                .HasColumnType("text");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses");

                            b1.ToJson("Title");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Module", b =>
                {
                    b.HasOne("Domain.Entities.Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.Base.Translation", "Description", b1 =>
                        {
                            b1.Property<Guid>("ModuleId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text");

                            b1.Property<string>("Kz")
                                .HasColumnType("text");

                            b1.Property<string>("Ru")
                                .HasColumnType("text");

                            b1.HasKey("ModuleId");

                            b1.ToTable("Modules");

                            b1.ToJson("Description");

                            b1.WithOwner()
                                .HasForeignKey("ModuleId");
                        });

                    b.OwnsOne("Domain.Entities.Base.Translation", "Title", b1 =>
                        {
                            b1.Property<Guid>("ModuleId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text");

                            b1.Property<string>("Kz")
                                .HasColumnType("text");

                            b1.Property<string>("Ru")
                                .HasColumnType("text");

                            b1.HasKey("ModuleId");

                            b1.ToTable("Modules");

                            b1.ToJson("Title");

                            b1.WithOwner()
                                .HasForeignKey("ModuleId");
                        });

                    b.Navigation("Course");

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.PracticeWork", b =>
                {
                    b.HasOne("Domain.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.Base.Translation", "Description", b1 =>
                        {
                            b1.Property<Guid>("PracticeWorkId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text")
                                .HasColumnName("DescriptionEn");

                            b1.Property<string>("Kz")
                                .HasColumnType("text")
                                .HasColumnName("DescriptionKz");

                            b1.Property<string>("Ru")
                                .HasColumnType("text")
                                .HasColumnName("DescriptionRu");

                            b1.HasKey("PracticeWorkId");

                            b1.ToTable("PracticeWorks");

                            b1.WithOwner()
                                .HasForeignKey("PracticeWorkId");
                        });

                    b.OwnsOne("Domain.Entities.Base.Translation", "Title", b1 =>
                        {
                            b1.Property<Guid>("PracticeWorkId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text");

                            b1.Property<string>("Kz")
                                .HasColumnType("text");

                            b1.Property<string>("Ru")
                                .HasColumnType("text");

                            b1.HasKey("PracticeWorkId");

                            b1.ToTable("PracticeWorks");

                            b1.ToJson("Title");

                            b1.WithOwner()
                                .HasForeignKey("PracticeWorkId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Question", b =>
                {
                    b.HasOne("Domain.Entities.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.Base.Translation", "Title", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text")
                                .HasColumnName("TitleEn");

                            b1.Property<string>("Kz")
                                .HasColumnType("text")
                                .HasColumnName("TitleKz");

                            b1.Property<string>("Ru")
                                .HasColumnType("text")
                                .HasColumnName("TitleRu");

                            b1.HasKey("QuestionId");

                            b1.ToTable("Questions");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");
                        });

                    b.Navigation("Test");

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Test", b =>
                {
                    b.HasOne("Domain.Entities.Module", "Module")
                        .WithMany("Tests")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.Base.Translation", "Title", b1 =>
                        {
                            b1.Property<Guid>("TestId")
                                .HasColumnType("uuid");

                            b1.Property<string>("En")
                                .HasColumnType("text");

                            b1.Property<string>("Kz")
                                .HasColumnType("text");

                            b1.Property<string>("Ru")
                                .HasColumnType("text");

                            b1.HasKey("TestId");

                            b1.ToTable("Tests");

                            b1.ToJson("Title");

                            b1.WithOwner()
                                .HasForeignKey("TestId");
                        });

                    b.Navigation("Module");

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Domain.Entities.Module", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("Domain.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Domain.Entities.Test", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
