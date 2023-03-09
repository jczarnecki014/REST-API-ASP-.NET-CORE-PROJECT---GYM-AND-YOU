using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymAndYou.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisterDayToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDay",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDay",
                table: "Users");
        }
    }
}
