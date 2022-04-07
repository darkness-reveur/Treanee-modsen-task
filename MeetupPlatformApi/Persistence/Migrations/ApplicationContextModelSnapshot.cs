﻿// <auto-generated />
using System;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetupPlatformApi.Migrations
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

                    b.HasIndex("OrganizerId");

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

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ux_users_username");

                    b.ToTable("users", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.Organizer", b =>
                {
                    b.HasBaseType("MeetupPlatformApi.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.PlainUser", b =>
                {
                    b.HasBaseType("MeetupPlatformApi.Domain.Users.User");

                    b.Property<Guid?>("MeetupId")
                        .HasColumnType("uuid")
                        .HasColumnName("meetup_id");

                    b.HasIndex("MeetupId");

                    b.HasDiscriminator().HasValue("PlainUser");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Meetup", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.Users.Organizer", null)
                        .WithMany("Meetups")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.SetNull)
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

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.PlainUser", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.Meetup", null)
                        .WithMany("Users")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_meetups_users_meetup_id");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Meetup", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Users.Organizer", b =>
                {
                    b.Navigation("Meetups");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.RefreshToken", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_refresh_tokens_user_id");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
