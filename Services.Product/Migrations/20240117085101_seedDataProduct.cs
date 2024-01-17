using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.Product.Migrations
{
    /// <inheritdoc />
    public partial class seedDataProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Mieso", "Najlepszy kotlet schabowy na swiecie.", "Kotlet Schabowy", 20.0 },
                    { 2, "Mieso", "Najlepszy kurczak z ryzem.", "Kurczak słodko-kwasny", 20.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);
        }
    }
}
