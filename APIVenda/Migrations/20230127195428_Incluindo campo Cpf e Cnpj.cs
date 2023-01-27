using Microsoft.EntityFrameworkCore.Migrations;

namespace APIVenda.Migrations
{
    public partial class IncluindocampoCpfeCnpj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Funcionarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Fornecedores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Clientes");
        }
    }
}
