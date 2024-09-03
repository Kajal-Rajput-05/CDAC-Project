using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistTypeColumnsInTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistType",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistType",
                table: "User");
        }
    }
}
