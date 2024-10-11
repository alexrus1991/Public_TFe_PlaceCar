using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlaceCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ajoutdesecujwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TRANS_Somme",
                table: "Trensaction",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FACT_Somme",
                table: "Facture",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Perm_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Perm_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Perm_id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonneRole",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PersonnePERS_Id = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonneRole", x => new { x.PersonneId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PersonneRole_Personne_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personne",
                        principalColumn: "PERS_Id");
                    table.ForeignKey(
                        name: "FK_PersonneRole_Personne_PersonnePERS_Id",
                        column: x => x.PersonnePERS_Id,
                        principalTable: "Personne",
                        principalColumn: "PERS_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonneRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Role_Id");
                    table.ForeignKey(
                        name: "FK_PersonneRole_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false),
                    PermissionPerm_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Perm_id");
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionPerm_id",
                        column: x => x.PermissionPerm_id,
                        principalTable: "Permission",
                        principalColumn: "Perm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Role_Id");
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Perm_id", "Perm_name" },
                values: new object[,]
                {
                    { 1, "read_admin" },
                    { 2, "write_admin" },
                    { 3, "read_parking" },
                    { 4, "write_parking" },
                    { 5, "read_all" },
                    { 6, "write_all" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Role_Id", "Role_Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Employee" },
                    { 3, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonneRole_PersonnePERS_Id",
                table: "PersonneRole",
                column: "PersonnePERS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonneRole_Role_Id",
                table: "PersonneRole",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonneRole_RoleId",
                table: "PersonneRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionPerm_id",
                table: "RolePermission",
                column: "PermissionPerm_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_Role_Id",
                table: "RolePermission",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonneRole");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.AlterColumn<decimal>(
                name: "TRANS_Somme",
                table: "Trensaction",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FACT_Somme",
                table: "Facture",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);
        }
    }
}
