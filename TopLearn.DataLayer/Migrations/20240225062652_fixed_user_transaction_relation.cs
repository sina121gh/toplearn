using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class fixed_user_transaction_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Transactions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                newName: "IX_Transactions_WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
