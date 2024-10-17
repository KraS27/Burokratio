using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notars",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    city = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    country = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    division = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    postal_code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    street = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notars", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notars");
        }
    }
}
