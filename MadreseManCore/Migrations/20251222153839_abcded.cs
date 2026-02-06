using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadreseManCore.Migrations
{
    /// <inheritdoc />
    public partial class abcded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "student_id",
                table: "report_card");

            migrationBuilder.RenameColumn(
                name: "exam_score",
                table: "report_card_entry",
                newName: "student_id");

            migrationBuilder.RenameColumn(
                name: "beahaviour_score",
                table: "report_card_entry",
                newName: "score");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "student_id",
                table: "report_card_entry",
                newName: "exam_score");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "report_card_entry",
                newName: "beahaviour_score");

            migrationBuilder.AddColumn<int>(
                name: "student_id",
                table: "report_card",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
