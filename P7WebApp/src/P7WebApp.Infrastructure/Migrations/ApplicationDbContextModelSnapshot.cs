﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using P7WebApp.Infrastructure.Persistence;

#nullable disable

namespace P7WebApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("ModuleSequence");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("CourseRoleId")
                        .HasColumnType("integer");

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("CourseRoleId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("OwnerId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.CourseRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDefaultRole")
                        .HasColumnType("boolean");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseRoles");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.InviteCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Code"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UseableFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("UseableTo")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.ToTable("InviteCode");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanAddExercise")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanAddExerciseGroup")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanCreateIniviteCode")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanCreateRoles")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDeleteExercise")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDeleteExerciseGroup")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDeleteSubmission")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanRemoveAttendee")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanRevokeInviteCode")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdateCourse")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdateExercise")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanViewSubmission")
                        .HasColumnType("boolean");

                    b.Property<int>("CourseRoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseRoleId")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ExerciseGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("ExerciseNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("LayoutId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("VisibleFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("VisibleTo")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseGroupId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule.TestCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CodeEditorModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("Test")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CodeEditorModuleId");

                    b.ToTable("TestCases");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"ModuleSequence\"')");

                    NpgsqlPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ExerciseId")
                        .HasColumnType("integer");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<int?>("SolutionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubmissionId")
                        .HasColumnType("integer");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("SolutionId");

                    b.HasIndex("SubmissionId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("QuizModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuizModuleId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("TextModuleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TextModuleId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Solution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("VisibleFromDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsSubmitted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("SubmitDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseGroupAggregate.ExerciseGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExerciseGroupNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("VisibleFromDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("ExerciseGroups");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ProfileAggregate.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("P7WebApp.Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule.CodeEditorModule", b =>
                {
                    b.HasBaseType("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.Module");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("CodeEditorModules");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.QuizModule", b =>
                {
                    b.HasBaseType("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.Module");

                    b.ToTable("QuizModules");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule.TextModule", b =>
                {
                    b.HasBaseType("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.Module");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("TextModules");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("P7WebApp.Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P7WebApp.Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Attendee", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.CourseAggregate.Course", null)
                        .WithMany("Attendees")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P7WebApp.Domain.Aggregates.CourseAggregate.CourseRole", "CourseRole")
                        .WithMany()
                        .HasForeignKey("CourseRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P7WebApp.Domain.Aggregates.ProfileAggregate.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseRole");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Course", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ProfileAggregate.Profile", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P7WebApp.Domain.Aggregates.ProfileAggregate.Profile", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LastModifiedBy");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.CourseRole", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.CourseAggregate.Course", null)
                        .WithMany("CourseRoles")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.InviteCode", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.CourseAggregate.Course", null)
                        .WithOne("InviteCode")
                        .HasForeignKey("P7WebApp.Domain.Aggregates.CourseAggregate.InviteCode", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Permission", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.CourseAggregate.CourseRole", null)
                        .WithOne("Permission")
                        .HasForeignKey("P7WebApp.Domain.Aggregates.CourseAggregate.Permission", "CourseRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Exercise", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseGroupAggregate.ExerciseGroup", null)
                        .WithMany("Exercises")
                        .HasForeignKey("ExerciseGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule.TestCase", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule.CodeEditorModule", null)
                        .WithMany("TestCases")
                        .HasForeignKey("CodeEditorModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.Module", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Exercise", "Exercise")
                        .WithMany("Modules")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Solution", "Solution")
                        .WithMany("Modules")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Submission", "Submission")
                        .WithMany("Modules")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Exercise");

                    b.Navigation("Solution");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.Choice", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.Question", null)
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.Question", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.QuizModule", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule.Image", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule.TextModule", null)
                        .WithMany("Images")
                        .HasForeignKey("TextModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Solution", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Exercise", null)
                        .WithMany("Solutions")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Submission", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.ExerciseAggregate.Exercise", null)
                        .WithMany("Submissions")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseGroupAggregate.ExerciseGroup", b =>
                {
                    b.HasOne("P7WebApp.Domain.Aggregates.CourseAggregate.Course", null)
                        .WithMany("ExerciseGroups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.Course", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("CourseRoles");

                    b.Navigation("ExerciseGroups");

                    b.Navigation("InviteCode");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.CourseAggregate.CourseRole", b =>
                {
                    b.Navigation("Permission")
                        .IsRequired();
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Exercise", b =>
                {
                    b.Navigation("Modules");

                    b.Navigation("Solutions");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.Question", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Solution", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Submission", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseGroupAggregate.ExerciseGroup", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule.CodeEditorModule", b =>
                {
                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule.QuizModule", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule.TextModule", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
