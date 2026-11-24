using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class affectedPeopleStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AffectedPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NeedsDescription",
                table: "AffectedPeople",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AffectedPeople");

            migrationBuilder.DropColumn(
                name: "NeedsDescription",
                table: "AffectedPeople");
        }
    }
}
