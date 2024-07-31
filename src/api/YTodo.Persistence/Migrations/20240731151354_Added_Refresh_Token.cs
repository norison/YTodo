using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YTodo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Refresh_Token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_RefreshToken",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_RefreshToken", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_tb_RefreshToken_tb_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_RefreshToken");
        }
    }
}
