﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "root_comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    meetup_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plain_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_root_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_root_comments_meetups_meetup_id",
                        column: x => x.meetup_id,
                        principalTable: "meetups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_root_comments_users_plain_user_id",
                        column: x => x.plain_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reply_comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    root_comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plain_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reply_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_reply_comments_root_comments_root_comment_id",
                        column: x => x.root_comment_id,
                        principalTable: "root_comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reply_comments_users_plain_user_id",
                        column: x => x.plain_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_reply_comments_plain_user_id",
                table: "reply_comments",
                column: "plain_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reply_comments_root_comment_id",
                table: "reply_comments",
                column: "root_comment_id");

            migrationBuilder.CreateIndex(
                name: "ix_root_comments_meetup_id",
                table: "root_comments",
                column: "meetup_id");

            migrationBuilder.CreateIndex(
                name: "ix_root_comments_plain_user_id",
                table: "root_comments",
                column: "plain_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reply_comments");

            migrationBuilder.DropTable(
                name: "root_comments");
        }
    }
}