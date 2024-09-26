using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateeeeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transazioni_Abbonamenti_TypeId",
                table: "Transazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Transazioni_Users_UserId",
                table: "Transazioni");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Transazioni",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Transazioni",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Transazioni_Abbonamenti_TypeId",
                table: "Transazioni",
                column: "TypeId",
                principalTable: "Abbonamenti",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transazioni_Users_UserId",
                table: "Transazioni",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transazioni_Abbonamenti_TypeId",
                table: "Transazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Transazioni_Users_UserId",
                table: "Transazioni");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Transazioni",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Transazioni",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transazioni_Abbonamenti_TypeId",
                table: "Transazioni",
                column: "TypeId",
                principalTable: "Abbonamenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transazioni_Users_UserId",
                table: "Transazioni",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
