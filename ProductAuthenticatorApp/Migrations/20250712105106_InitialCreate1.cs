using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAuthenticatorApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Clients_ClientId1",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_ClientId1",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Leases");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Leases",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_ClientId",
                table: "Leases",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Clients_ClientId",
                table: "Leases",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Clients_ClientId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_ClientId",
                table: "Leases");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Leases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leases_ClientId1",
                table: "Leases",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Clients_ClientId1",
                table: "Leases",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
