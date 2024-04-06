using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniVerse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyToJobOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "JobOffers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "JobOffers");
        }
    }
}
