using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Semih.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorIdToConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Conversations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_DoctorId",
                table: "Conversations",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Conversations_DoctorId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Conversations");
        }
    }
}
