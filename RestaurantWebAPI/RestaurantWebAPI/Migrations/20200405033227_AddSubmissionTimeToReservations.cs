using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantWebAPI.Migrations
{
    public partial class AddSubmissionTimeToReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionTime",
                table: "TableReservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionTime",
                table: "TableReservations");
        }
    }
}
