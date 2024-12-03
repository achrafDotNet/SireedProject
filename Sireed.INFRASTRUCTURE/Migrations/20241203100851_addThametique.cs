using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sireed.INFRASTRUCTURE.Migrations
{
    public partial class addThametique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgricultureEtindustrie",
                table: "Thematiques");

            migrationBuilder.DropColumn(
                name: "Air",
                table: "Thematiques");

            migrationBuilder.DropColumn(
                name: "Dechets",
                table: "Thematiques");

            migrationBuilder.DropColumn(
                name: "EauEtassainissement",
                table: "Thematiques");

            migrationBuilder.DropColumn(
                name: "LittoralEtbiodiversité",
                table: "Thematiques");

            migrationBuilder.RenameColumn(
                name: "PopulationEtEducationEnvironnementale",
                table: "Thematiques",
                newName: "Nom");

            migrationBuilder.CreateTable(
                name: "descriptionThematiques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheMatId = table.Column<int>(type: "int", nullable: false),
                    DescriptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThematiqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptionThematiques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_descriptionThematiques_Thematiques_ThematiqueId",
                        column: x => x.ThematiqueId,
                        principalTable: "Thematiques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_descriptionThematiques_ThematiqueId",
                table: "descriptionThematiques",
                column: "ThematiqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "descriptionThematiques");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Thematiques",
                newName: "PopulationEtEducationEnvironnementale");

            migrationBuilder.AddColumn<string>(
                name: "AgricultureEtindustrie",
                table: "Thematiques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Air",
                table: "Thematiques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dechets",
                table: "Thematiques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EauEtassainissement",
                table: "Thematiques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LittoralEtbiodiversité",
                table: "Thematiques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
