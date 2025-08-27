using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Semih.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserExtraProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AbpUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "AbpUsers",
                type: "double precision",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "AbpUsers",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "AbpUsers",
                type: "double precision",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AbpUsers");
        }
    }
}
