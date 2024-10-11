using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modificationdesliensentrelestablesrolesetpermissionetpersonnepourdu1n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonneRole_Personne_PersonneId",
                table: "PersonneRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonneRole_Personne_PersonnePERS_Id",
                table: "PersonneRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonneRole_Role_RoleId",
                table: "PersonneRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonneRole_Role_Role_Id",
                table: "PersonneRole");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionPerm_id",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_Role_Id",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_PermissionPerm_id",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_Role_Id",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonneRole",
                table: "PersonneRole");

            migrationBuilder.DropIndex(
                name: "IX_PersonneRole_PersonnePERS_Id",
                table: "PersonneRole");

            migrationBuilder.DropIndex(
                name: "IX_PersonneRole_Role_Id",
                table: "PersonneRole");

            migrationBuilder.DropColumn(
                name: "PermissionPerm_id",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "PersonnePERS_Id",
                table: "PersonneRole");

            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "PersonneRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                columns: new[] { "RoleId", "PermissionId" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonneRole",
                table: "PersonneRole",
                columns: new[] { "PersonneId", "RoleId" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonneRole_Personne_PersonneId",
                table: "PersonneRole",
                column: "PersonneId",
                principalTable: "Personne",
                principalColumn: "PERS_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonneRole_Role_RoleId",
                table: "PersonneRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Role_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Perm_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Role_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonneRole_Personne_PersonneId",
                table: "PersonneRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonneRole_Role_RoleId",
                table: "PersonneRole");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonneRole",
                table: "PersonneRole");

            migrationBuilder.AddColumn<int>(
                name: "PermissionPerm_id",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonnePERS_Id",
                table: "PersonneRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "PersonneRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonneRole",
                table: "PersonneRole",
                columns: new[] { "PersonneId", "RoleId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_PersonneRole_PersonnePERS_Id",
                table: "PersonneRole",
                column: "PersonnePERS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonneRole_Role_Id",
                table: "PersonneRole",
                column: "Role_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonneRole_Personne_PersonneId",
                table: "PersonneRole",
                column: "PersonneId",
                principalTable: "Personne",
                principalColumn: "PERS_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonneRole_Personne_PersonnePERS_Id",
                table: "PersonneRole",
                column: "PersonnePERS_Id",
                principalTable: "Personne",
                principalColumn: "PERS_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonneRole_Role_RoleId",
                table: "PersonneRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Role_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonneRole_Role_Role_Id",
                table: "PersonneRole",
                column: "Role_Id",
                principalTable: "Role",
                principalColumn: "Role_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Perm_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionPerm_id",
                table: "RolePermission",
                column: "PermissionPerm_id",
                principalTable: "Permission",
                principalColumn: "Perm_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Role_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_Role_Id",
                table: "RolePermission",
                column: "Role_Id",
                principalTable: "Role",
                principalColumn: "Role_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
