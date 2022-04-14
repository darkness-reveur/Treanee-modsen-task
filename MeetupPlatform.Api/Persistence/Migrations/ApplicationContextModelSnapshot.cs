﻿// <auto-generated />
using System;
using MeetupPlatform.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetupPlatform.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeetupPlatformApi.Domain.Meetup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uuid")
                        .HasColumnName("organizer_id");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_meetups");

                    b.HasIndex("OrganizerId")
                        .HasDatabaseName("ix_meetups_organizer_id");

                    b.ToTable("meetups", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_refresh_tokens");

                    b.HasIndex("UserId");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.Property<string>("role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("PlainUser");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ux_users_username");

                    b.ToTable("users", (string)null);

                    b.HasDiscriminator<string>("role").HasValue("User");
                });

            modelBuilder.Entity("meetups_users_signups", b =>
                {
                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("meetup_id")
                        .HasColumnType("uuid");

                    b.HasKey("user_id", "meetup_id")
                        .HasName("pk_meetups_users_signup");

                    b.HasIndex("meetup_id")
                        .HasDatabaseName("ix_meetups_users_signups_meetup_id");

                    b.HasIndex("user_id")
                        .HasDatabaseName("ix_meetups_users_signups_user_id");

                    b.ToTable("meetups_users_signups", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.Organizer", b =>
                {
                    b.HasBaseType("MeetupPlatformApi.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.PlainUser", b =>
                {
                    b.HasBaseType("MeetupPlatformApi.Domain.Users.User");

                    b.HasDiscriminator().HasValue("PlainUser");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Meetup", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.Users.Organizer", null)
                        .WithMany("OrganizedMeetups")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_meetups_organizer_id");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.RefreshToken", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.Users.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_refresh_tokens_user_id");
                });

            modelBuilder.Entity("meetups_users_signups", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.Meetup", null)
                        .WithMany()
                        .HasForeignKey("meetup_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meetups_users_signups_meetups_meetup_id");

                    b.HasOne("MeetupPlatformApi.Domain.Users.PlainUser", null)
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meetups_users_signups_plain_users_user_id");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.Organizer", b =>
                {
                    b.Navigation("OrganizedMeetups");
                });
#pragma warning restore 612, 618
        }
    }
}
