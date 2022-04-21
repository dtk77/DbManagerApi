using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbManagerApi.Migrations
{
    public partial class DatabaseCreation01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { new Guid("3d11132e-6371-4e30-8a8d-3738eb99dbbf"), "Description product 1", "Product 1" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { new Guid("8a997804-7308-45a8-a3e6-ed65bfab46e8"), "Description product 2", "Product 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("3d11132e-6371-4e30-8a8d-3738eb99dbbf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("8a997804-7308-45a8-a3e6-ed65bfab46e8"));
        }
    }
}
