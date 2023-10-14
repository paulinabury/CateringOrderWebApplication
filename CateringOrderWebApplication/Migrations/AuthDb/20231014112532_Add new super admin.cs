using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CateringOrderWebApplication.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Addnewsuperadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d2630249-33c6-4090-8eb9-635473e43dd4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94331f19-df82-4db1-a9e7-23cf3ec7fb46", "AQAAAAIAAYagAAAAEId2HoFDzbozydrcNTFAhIkMQ7EuLw+jIL+UU8h7pORPQN3d5jAIDg0Wb1nDAAQClA==", "2340efc2-1707-44ba-ad7f-5723ca14c067" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9AD8AF12-6961-4669-A618-23DAC01141B3", 0, "be0381f1-a456-4bef-bed6-793b9cefeac2", "superAdmin@wp.pl", true, false, null, "SUPERADMIN@WP.PL", "SUPERADMIN", "AQAAAAIAAYagAAAAELxdVpZ9HKB6LmaJmtfeBloJZRiUxEX7Jc2t7UMB577RLXG89lMlZr2RqZWGJcx4rA==", "123456789", true, "56f20348-7a6c-4585-b189-aa599ad59d8b", false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "02c80728-fca5-47ad-813d-e409ebaa5251", "9AD8AF12-6961-4669-A618-23DAC01141B3" },
                    { "035eacd4-fd1d-496b-b857-1bcfc8438cb4", "9AD8AF12-6961-4669-A618-23DAC01141B3" },
                    { "a0329a66-e162-4d14-88f8-ee80d931ac11", "9AD8AF12-6961-4669-A618-23DAC01141B3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "02c80728-fca5-47ad-813d-e409ebaa5251", "9AD8AF12-6961-4669-A618-23DAC01141B3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "035eacd4-fd1d-496b-b857-1bcfc8438cb4", "9AD8AF12-6961-4669-A618-23DAC01141B3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a0329a66-e162-4d14-88f8-ee80d931ac11", "9AD8AF12-6961-4669-A618-23DAC01141B3" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9AD8AF12-6961-4669-A618-23DAC01141B3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d2630249-33c6-4090-8eb9-635473e43dd4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f133200-0745-4d3e-a682-19d2d480f6f4", "AQAAAAIAAYagAAAAEHcBF3Ex5PWgrgMsHydnFr9VN/FJvy37zLoaaoUL+lwg8FtqsZx+eIGCQltyPdtAcA==", "d3d8dc36-747b-4189-92c6-3241b8c537ae" });
        }
    }
}
