using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbManagerApi.Migrations
{
    public partial class DatabaseCreation02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { new Guid("8a997804-0308-45a8-a3e6-ed64bfab46e9"), "Description product 3", "Product 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("8a997804-0308-45a8-a3e6-ed64bfab46e9"));
        }
    }
}
