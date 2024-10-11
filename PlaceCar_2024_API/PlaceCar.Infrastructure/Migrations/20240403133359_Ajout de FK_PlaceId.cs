using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjoutdeFK_PlaceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Places_PlaceParkingPLA_Id",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PlaceParkingPLA_Id",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PlaceParkingPLA_Id",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PlaceId",
                table: "Reservation",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Places_PlaceId",
                table: "Reservation",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "PLA_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Places_PlaceId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PlaceId",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "PlaceParkingPLA_Id",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PlaceParkingPLA_Id",
                table: "Reservation",
                column: "PlaceParkingPLA_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Places_PlaceParkingPLA_Id",
                table: "Reservation",
                column: "PlaceParkingPLA_Id",
                principalTable: "Places",
                principalColumn: "PLA_Id");
        }
    }
}
