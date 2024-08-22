using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank_Wiki_React_ASP_App.Server.Migrations
{
    /// <inheritdoc />
    public partial class NewCreatePictureAndTankPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Nations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureId);
                });

            migrationBuilder.CreateTable(
                name: "TankPictures",
                columns: table => new
                {
                    TankId = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankPictures", x => new { x.TankId, x.PictureId });
                    table.ForeignKey(
                        name: "FK_TankPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TankPictures_Tanks_TankId",
                        column: x => x.TankId,
                        principalTable: "Tanks",
                        principalColumn: "TankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nations_PictureId",
                table: "Nations",
                column: "PictureId",
                unique: true,
                filter: "[PictureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TankPictures_PictureId",
                table: "TankPictures",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nations_Pictures_PictureId",
                table: "Nations",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "PictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nations_Pictures_PictureId",
                table: "Nations");

            migrationBuilder.DropTable(
                name: "TankPictures");

            migrationBuilder.DropIndex(
                name: "IX_Nations_PictureId",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Nations");
        }
    }
}
