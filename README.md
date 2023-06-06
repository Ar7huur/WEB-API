CRUD C# - EF e SQL Server
Comando para popular banco de dados em VS 2022:

Remove-Migration
Update-Migration Inital
Update-Database


namespace ProjCsharp.Migrations {
    /// <inheritdoc />
    public partial class PopularDepartaments : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Computers')");
            migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Eletronics')");
            migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Fashion')");
            migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Books')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("DELETE FROM Departaments");
        }
    }
}
