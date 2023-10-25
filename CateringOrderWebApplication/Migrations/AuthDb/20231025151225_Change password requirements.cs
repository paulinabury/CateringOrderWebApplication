using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringOrderWebApplication.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Changepasswordrequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d2630249-33c6-4090-8eb9-635473e43dd4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20bb1a34-c59a-40f3-a147-135a4be679cc", "AQAAAAIAAYagAAAAEI18IsEq5pi6dCisFZl/ulFXLQ88MlxUgHcaFsenaoCTBFO0UTWIIL3TiSB0injzTg==", "1338551f-857e-4832-a0b2-1c979d507ae6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d2630249-33c6-4090-8eb9-635473e43dd4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55e9aa1a-fd4e-455b-892b-77a9125331a4", "AQAAAAIAAYagAAAAEAkVGR597mdbgvnH5LRYWEZMadoRsjTO/RDeqVEzDYnCK62LQoBw6ewQU9sfRzKTlA==", "6c4267af-3e58-4558-acc9-8472466984b0" });
        }
    }
}
