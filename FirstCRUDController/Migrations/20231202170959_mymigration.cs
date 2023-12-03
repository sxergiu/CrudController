using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstCRUDController.Migrations
{
    /// <inheritdoc />
    public partial class mymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaidId",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Maids",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maids", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MaidId",
                table: "Rooms",
                column: "MaidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Maids_MaidId",
                table: "Rooms",
                column: "MaidId",
                principalTable: "Maids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Maids_MaidId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Maids");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_MaidId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MaidId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Rooms");
        }
    }
}
