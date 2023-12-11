using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademiaProjetoPOO.Migrations
{
    /// <inheritdoc />
    public partial class IdadeAddedToAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Alunos");
        }
    }
}
