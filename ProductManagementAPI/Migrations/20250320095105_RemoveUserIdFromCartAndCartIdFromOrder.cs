using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class RemoveUserIdFromCartAndCartIdFromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the foreign key exists before dropping
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Carts_Users_UserId') " +
                                 "ALTER TABLE Carts DROP CONSTRAINT FK_Carts_Users_UserId;");
            
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Orders_Carts_CartId') " +
                                 "ALTER TABLE Orders DROP CONSTRAINT FK_Orders_Carts_CartId;");

            // Drop indexes safely if they exist
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_CartId') " +
                                 "DROP INDEX IX_Orders_CartId ON Orders;");
            
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Carts_UserId') " +
                                 "DROP INDEX IX_Carts_UserId ON Carts;");

            // Drop the columns
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add columns
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Recreate indexes
            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            // Add foreign keys back
            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
