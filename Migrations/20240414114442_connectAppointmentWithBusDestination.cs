using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBooking.DotNet.Migrations
{
    /// <inheritdoc />
    public partial class connectAppointmentWithBusDestination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BusDestinationId",
                table: "Appointments",
                column: "BusDestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_BusDestinations_BusDestinationId",
                table: "Appointments",
                column: "BusDestinationId",
                principalTable: "BusDestinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_BusDestinations_BusDestinationId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_BusDestinationId",
                table: "Appointments");
        }
    }
}
