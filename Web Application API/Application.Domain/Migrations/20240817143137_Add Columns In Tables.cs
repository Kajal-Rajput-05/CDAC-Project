using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsInTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentDetail",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDetail",
                table: "Event");
        }
    }
}
