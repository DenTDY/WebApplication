using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77ed95cd-219a-4e04-a288-ae4daaec7237", "cfea983f-5d60-4b0a-8920-c0156701f8da", "Administrator", "ADMINISTRATOR" },
                    { "d10d2fcf-aadd-4b06-ad01-cc5bfb12d103", "4662bb7b-48a4-40c2-a140-f6515f04173b", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77ed95cd-219a-4e04-a288-ae4daaec7237");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d10d2fcf-aadd-4b06-ad01-cc5bfb12d103");
        }
    }
}
