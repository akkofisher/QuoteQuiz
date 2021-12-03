using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuoteQuiz.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuoteText = table.Column<string>(nullable: true),
                    Mode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CurrentMode = table.Column<int>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Answers_Binary",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteID = table.Column<int>(nullable: false),
                    CorrectAnswer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers_Binary", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answers_Binary_Quotes_QuoteID",
                        column: x => x.QuoteID,
                        principalTable: "Quotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers_Multiple",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteID = table.Column<int>(nullable: false),
                    PossibleAnwerText = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers_Multiple", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answers_Multiple_Quotes_QuoteID",
                        column: x => x.QuoteID,
                        principalTable: "Quotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Answers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    QuoteID = table.Column<int>(nullable: false),
                    UserMultipleAnswerID = table.Column<int>(nullable: true),
                    UserBinaryAnswer = table.Column<bool>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Answers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Answers_Quotes_QuoteID",
                        column: x => x.QuoteID,
                        principalTable: "Quotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Answers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Binary_QuoteID",
                table: "Answers_Binary",
                column: "QuoteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Multiple_QuoteID",
                table: "Answers_Multiple",
                column: "QuoteID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Answers_QuoteID",
                table: "User_Answers",
                column: "QuoteID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Answers_UserID",
                table: "User_Answers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers_Binary");

            migrationBuilder.DropTable(
                name: "Answers_Multiple");

            migrationBuilder.DropTable(
                name: "User_Answers");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
