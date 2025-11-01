using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge24.Data.Migrations
{

    public partial class AddKindergartenFiles : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KindergartenFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KindergartenId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindergartenFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KindergartenFiles_Kindergartens_KindergartenId",
                        column: x => x.KindergartenId,
                        principalTable: "Kindergartens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KindergartenFiles_KindergartenId",
                table: "KindergartenFiles",
                column: "KindergartenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KindergartenFiles");
        }
    }
}
