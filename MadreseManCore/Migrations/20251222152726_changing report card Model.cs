using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadreseManCore.Migrations
{
    /// <inheritdoc />
    public partial class changingreportcardModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "class_id",
                table: "report_card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "report_card",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "class_id",
                table: "report_card");

            migrationBuilder.DropColumn(
                name: "title",
                table: "report_card");
        }
    }
}
