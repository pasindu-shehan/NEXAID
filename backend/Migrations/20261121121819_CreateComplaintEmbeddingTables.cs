using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateComplaintEmbeddingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintEmbeddings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    EmbeddingJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintEmbeddings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintEmbeddings_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DuplicateComplaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalComplaintId = table.Column<int>(type: "int", nullable: false),
                    DuplicateComplaintId = table.Column<int>(type: "int", nullable: false),
                    SimilarityScore = table.Column<double>(type: "float", nullable: false),
                    ReviewStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuplicateComplaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DuplicateComplaints_Complaints_DuplicateComplaintId",
                        column: x => x.DuplicateComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DuplicateComplaints_Complaints_OriginalComplaintId",
                        column: x => x.OriginalComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintEmbeddings_ComplaintId",
                table: "ComplaintEmbeddings",
                column: "ComplaintId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateComplaints_DuplicateComplaintId",
                table: "DuplicateComplaints",
                column: "DuplicateComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateComplaints_OriginalComplaintId_DuplicateComplaintId",
                table: "DuplicateComplaints",
                columns: new[] { "OriginalComplaintId", "DuplicateComplaintId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintEmbeddings");

            migrationBuilder.DropTable(
                name: "DuplicateComplaints");

            migrationBuilder.DropTable(
                name: "Complaints");
        }
    }
}
