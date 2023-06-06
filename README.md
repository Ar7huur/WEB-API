CRUD C# - EF e SQL Server

Remove-Migration
Update-Migration Inital
Update-Database

Comandos para popular o banco de dados pelo Visual Studio 2022.
protected override void Up(MigrationBuilder migrationBuilder) {
migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
"VALUES ('Computers')");
migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Eletronics')");
migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Fashion')");
migrationBuilder.Sql("INSERT INTO Departaments(Name)" +
                "VALUES ('Books')");
  
