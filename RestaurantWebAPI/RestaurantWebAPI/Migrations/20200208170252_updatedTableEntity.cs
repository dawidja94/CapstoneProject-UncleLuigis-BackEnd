using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantWebAPI.Migrations
{
    public partial class updatedTableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "TableReservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "TableReservations");
        }
    }
}
