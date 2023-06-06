using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopVen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Vendas(VendaData,VendaValor)" +
                    "VALUES ('18/09/2012', 15000.0)");
            migrationBuilder.Sql("INSERT INTO Vendas(VendaData,VendaValor)" +
                "VALUES ('18/12/2014', 19000.0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Vendas");
        }
    }
}
