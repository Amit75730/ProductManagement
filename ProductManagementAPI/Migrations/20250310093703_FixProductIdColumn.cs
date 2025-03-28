﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixProductIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId1",
                table: "Products",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductId1",
                table: "Products",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Products");
        }
    }
}
