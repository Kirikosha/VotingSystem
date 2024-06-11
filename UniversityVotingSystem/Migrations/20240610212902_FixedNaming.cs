using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityVotingSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixedNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Voting",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ends_at",
                table: "Voting",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "starts_at",
                table: "Voting",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "Voting",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Voting");

            migrationBuilder.DropColumn(
                name: "ends_at",
                table: "Voting");

            migrationBuilder.DropColumn(
                name: "starts_at",
                table: "Voting");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Voting");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false);
        }
    }
}
