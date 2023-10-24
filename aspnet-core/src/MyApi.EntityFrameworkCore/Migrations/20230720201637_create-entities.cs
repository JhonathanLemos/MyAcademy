using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApi.Migrations
{
    public partial class createentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataNascimento",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "AbpUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TreinoId",
                table: "AbpUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EmailCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailCodes_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treinos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnosTreino = table.Column<int>(type: "int", nullable: false),
                    CondicionamentoTreino = table.Column<int>(type: "int", nullable: false),
                    DiasTreino = table.Column<int>(type: "int", nullable: false),
                    HorariosTreino = table.Column<int>(type: "int", nullable: false),
                    ObjetivoTreino = table.Column<int>(type: "int", nullable: false),
                    RestricaoAlimentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutraInformacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treinos_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailCodes_UserId",
                table: "EmailCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Treinos_UserId",
                table: "Treinos",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailCodes");

            migrationBuilder.DropTable(
                name: "Treinos");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "TreinoId",
                table: "AbpUsers");
        }
    }
}
