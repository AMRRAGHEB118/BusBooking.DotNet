using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBooking.DotNet.Migrations
{
    /// <inheritdoc />
    public partial class seeRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: ["Id", "ConcurrencyStamp", "Name", "NormalizedName"],
                values: [Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Admin", "ADMIN"]
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: ["Id", "ConcurrencyStamp", "Name", "NormalizedName"],
                values: [Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "traveler", "TRAVELER"]
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
