using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank_Wiki_React_ASP_App.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedPictureInNation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pictures",
                newName: "PictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Pictures",
                newName: "Id");
        }
    }
}
