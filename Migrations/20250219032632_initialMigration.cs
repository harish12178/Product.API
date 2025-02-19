using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeiss.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'ProductIdSequence')
                BEGIN
                    CREATE SEQUENCE ProductIdSequence
                    START WITH 100000
                    INCREMENT BY 1
                    MINVALUE 100000
                    MAXVALUE 999999
                    CYCLE  -- Or NO CYCLE
                    CACHE 20; -- Adjust as needed
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.Sql(@"
                DROP SEQUENCE ProductIdSequence;
            ");
        }
    }
}
