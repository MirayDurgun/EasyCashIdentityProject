using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCashIdentityProject.DataAccessLayer.Migrations
{
    public partial class mig_add_customer_relation_process : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverID",
                table: "CustomerAccountsProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderID",
                table: "CustomerAccountsProcess",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountsProcess_ReceiverID",
                table: "CustomerAccountsProcess",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountsProcess_SenderID",
                table: "CustomerAccountsProcess",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountsProcess_CustomerAccounts_ReceiverID",
                table: "CustomerAccountsProcess",
                column: "ReceiverID",
                principalTable: "CustomerAccounts",
                principalColumn: "CustomerAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountsProcess_CustomerAccounts_SenderID",
                table: "CustomerAccountsProcess",
                column: "SenderID",
                principalTable: "CustomerAccounts",
                principalColumn: "CustomerAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountsProcess_CustomerAccounts_ReceiverID",
                table: "CustomerAccountsProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountsProcess_CustomerAccounts_SenderID",
                table: "CustomerAccountsProcess");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountsProcess_ReceiverID",
                table: "CustomerAccountsProcess");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountsProcess_SenderID",
                table: "CustomerAccountsProcess");

            migrationBuilder.DropColumn(
                name: "ReceiverID",
                table: "CustomerAccountsProcess");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "CustomerAccountsProcess");
        }
    }
}
