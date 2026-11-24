using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToComplaints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReviewStatus",
                table: "DuplicateComplaints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "ComplaintsId",
                table: "DuplicateComplaints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComplaintsId1",
                table: "DuplicateComplaints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "ComplaintEmbeddings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateComplaints_ComplaintsId",
                table: "DuplicateComplaints",
                column: "ComplaintsId");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateComplaints_ComplaintsId1",
                table: "DuplicateComplaints",
                column: "ComplaintsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DuplicateComplaints_Complaints_ComplaintsId",
                table: "DuplicateComplaints",
                column: "ComplaintsId",
                principalTable: "Complaints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DuplicateComplaints_Complaints_ComplaintsId1",
                table: "DuplicateComplaints",
                column: "ComplaintsId1",
                principalTable: "Complaints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DuplicateComplaints_Complaints_ComplaintsId",
                table: "DuplicateComplaints");

            migrationBuilder.DropForeignKey(
                name: "FK_DuplicateComplaints_Complaints_ComplaintsId1",
                table: "DuplicateComplaints");

            migrationBuilder.DropIndex(
                name: "IX_DuplicateComplaints_ComplaintsId",
                table: "DuplicateComplaints");

            migrationBuilder.DropIndex(
                name: "IX_DuplicateComplaints_ComplaintsId1",
                table: "DuplicateComplaints");

            migrationBuilder.DropColumn(
                name: "ComplaintsId",
                table: "DuplicateComplaints");

            migrationBuilder.DropColumn(
                name: "ComplaintsId1",
                table: "DuplicateComplaints");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Complaints");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewStatus",
                table: "DuplicateComplaints",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "ComplaintEmbeddings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
