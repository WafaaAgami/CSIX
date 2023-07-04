using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSIX.Migrations
{
    /// <inheritdoc />
    public partial class ActivitiesUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Activities",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Activities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Activities",
                newName: "Details");
        }
    }
}
