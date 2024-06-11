using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityVotingSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixedVotingPropositionsDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposition_Voting_voting_id",
                table: "Proposition");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersVote_Proposition_proposition_id",
                table: "UsersVote");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposition_Voting_voting_id",
                table: "Proposition",
                column: "voting_id",
                principalTable: "Voting",
                principalColumn: "voting_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersVote_Proposition_proposition_id",
                table: "UsersVote",
                column: "proposition_id",
                principalTable: "Proposition",
                principalColumn: "proposition_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposition_Voting_voting_id",
                table: "Proposition");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersVote_Proposition_proposition_id",
                table: "UsersVote");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposition_Voting_voting_id",
                table: "Proposition",
                column: "voting_id",
                principalTable: "Voting",
                principalColumn: "voting_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersVote_Proposition_proposition_id",
                table: "UsersVote",
                column: "proposition_id",
                principalTable: "Proposition",
                principalColumn: "proposition_id");
        }
    }
}
