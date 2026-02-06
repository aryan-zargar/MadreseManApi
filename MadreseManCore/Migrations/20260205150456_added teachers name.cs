using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadreseManCore.Migrations
{
    /// <inheritdoc />
    public partial class addedteachersname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "teacher_name",
                table: "subject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "teacher_name",
                table: "subject");
        }
    }
}
