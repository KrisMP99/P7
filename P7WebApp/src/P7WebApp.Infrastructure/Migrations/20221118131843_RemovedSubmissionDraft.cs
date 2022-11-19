using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace P7WebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSubmissionDraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeEditorModules_SubmissionsDrafts_SubmissionDraftId",
                table: "CodeEditorModules");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizModules_SubmissionsDrafts_SubmissionDraftId",
                table: "QuizModules");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_SubmissionsDrafts_SubmissionDraftId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TextModules_SubmissionsDrafts_SubmissionDraftId",
                table: "TextModules");

            migrationBuilder.DropTable(
                name: "SubmissionsDrafts");

            migrationBuilder.DropIndex(
                name: "IX_TextModules_SubmissionDraftId",
                table: "TextModules");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_SubmissionDraftId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_QuizModules_SubmissionDraftId",
                table: "QuizModules");

            migrationBuilder.DropIndex(
                name: "IX_CodeEditorModules_SubmissionDraftId",
                table: "CodeEditorModules");

            migrationBuilder.DropColumn(
                name: "SubmissionDraftId",
                table: "TextModules");

            migrationBuilder.DropColumn(
                name: "SubmissionDraftId",
                table: "QuizModules");

            migrationBuilder.DropColumn(
                name: "SubmissionDraftId",
                table: "CodeEditorModules");

            migrationBuilder.RenameColumn(
                name: "SubmissionDraftId",
                table: "Submissions",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Submissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextModuleId = table.Column<int>(type: "integer", nullable: false),
                    File = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_TextModules_TextModuleId",
                        column: x => x.TextModuleId,
                        principalTable: "TextModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_TextModuleId",
                table: "Image",
                column: "TextModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Submissions",
                newName: "SubmissionDraftId");

            migrationBuilder.AddColumn<int>(
                name: "SubmissionDraftId",
                table: "TextModules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmissionDraftId",
                table: "QuizModules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmissionDraftId",
                table: "CodeEditorModules",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubmissionsDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionsDrafts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextModules_SubmissionDraftId",
                table: "TextModules",
                column: "SubmissionDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_SubmissionDraftId",
                table: "Submissions",
                column: "SubmissionDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizModules_SubmissionDraftId",
                table: "QuizModules",
                column: "SubmissionDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeEditorModules_SubmissionDraftId",
                table: "CodeEditorModules",
                column: "SubmissionDraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeEditorModules_SubmissionsDrafts_SubmissionDraftId",
                table: "CodeEditorModules",
                column: "SubmissionDraftId",
                principalTable: "SubmissionsDrafts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizModules_SubmissionsDrafts_SubmissionDraftId",
                table: "QuizModules",
                column: "SubmissionDraftId",
                principalTable: "SubmissionsDrafts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_SubmissionsDrafts_SubmissionDraftId",
                table: "Submissions",
                column: "SubmissionDraftId",
                principalTable: "SubmissionsDrafts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextModules_SubmissionsDrafts_SubmissionDraftId",
                table: "TextModules",
                column: "SubmissionDraftId",
                principalTable: "SubmissionsDrafts",
                principalColumn: "Id");
        }
    }
}
