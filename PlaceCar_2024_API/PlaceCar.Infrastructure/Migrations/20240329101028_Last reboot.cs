using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Lastreboot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    FACT_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FACT_Somme = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    FACT_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.FACT_Id);
                });

            migrationBuilder.CreateTable(
                name: "FormulesPrixType",
                columns: table => new
                {
                    FORM_Type_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORM_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FORM_Type_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulesPrixType", x => x.FORM_Type_Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoEntreprise",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Cb_NumCompte = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoEntreprise", x => x.Nom);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    PAYS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAYS_Nom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.PAYS_Id);
                });

            migrationBuilder.CreateTable(
                name: "Personne",
                columns: table => new
                {
                    PERS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PERS_Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PERS_Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PERS_DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PERS_Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PERS_Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personne", x => x.PERS_Id);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    ADRS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADRS_Numero = table.Column<int>(type: "int", nullable: false),
                    ADRS_NomRue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADRS_Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADRS_Latitude = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    ADRS_Longitude = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    PaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.ADRS_Id);
                    table.ForeignKey(
                        name: "FK_Adresses_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "PAYS_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Cli_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Cli_Id);
                    table.ForeignKey(
                        name: "FK_Client_Personne",
                        column: x => x.Cli_Id,
                        principalTable: "Personne",
                        principalColumn: "PERS_Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Emp_Pers_Id = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Emp_Pers_Id);
                    table.ForeignKey(
                        name: "FK_Employee_Personne",
                        column: x => x.Emp_Pers_Id,
                        principalTable: "Personne",
                        principalColumn: "PERS_Id");
                });

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    PARK_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PARK_Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PARK_NbEtages = table.Column<int>(type: "int", nullable: false),
                    PARK_NbPlaces = table.Column<int>(type: "int", nullable: false),
                    AdreseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.PARK_Id);
                    table.ForeignKey(
                        name: "FK_Parkings_Adresses_AdreseId",
                        column: x => x.AdreseId,
                        principalTable: "Adresses",
                        principalColumn: "ADRS_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompteBank",
                columns: table => new
                {
                    CB_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CB_Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CB_NumCompte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CB_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompteBank", x => x.CB_Id);
                    table.ForeignKey(
                        name: "FK_Comptes_Client",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Cli_Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeWorkOn",
                columns: table => new
                {
                    Emp_Pers_Id = table.Column<int>(type: "int", nullable: false),
                    ParkingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employe_parking", x => new { x.Emp_Pers_Id, x.ParkingId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_EmployeWorkOn_Employee_Emp_Pers_Id",
                        column: x => x.Emp_Pers_Id,
                        principalTable: "Employee",
                        principalColumn: "Emp_Pers_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeWorkOn_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "PARK_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormuleDePrix",
                columns: table => new
                {
                    FORM_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORM_Prix = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ParkingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormuleDePrix", x => x.FORM_Id);
                    table.ForeignKey(
                        name: "FK_FormuleDePrix_FormulesPrixType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FormulesPrixType",
                        principalColumn: "FORM_Type_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormuleDePrix_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "PARK_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PLA_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PLA_Etage = table.Column<int>(type: "int", nullable: false),
                    PLA_NumeroPlace = table.Column<int>(type: "int", nullable: false),
                    ParkingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PLA_Id);
                    table.ForeignKey(
                        name: "FK_Places_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "PARK_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trensaction",
                columns: table => new
                {
                    TRANS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRANS_Somme = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    TRANS_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TRANS_Communication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactureId = table.Column<int>(type: "int", nullable: false),
                    CompteUnId = table.Column<int>(type: "int", nullable: false),
                    CompteEntreprise = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trensaction", x => x.TRANS_Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Comptes",
                        column: x => x.CompteUnId,
                        principalTable: "CompteBank",
                        principalColumn: "CB_Id");
                    table.ForeignKey(
                        name: "FK_Transactions_InfoEntreprise",
                        column: x => x.CompteEntreprise,
                        principalTable: "InfoEntreprise",
                        principalColumn: "Nom");
                    table.ForeignKey(
                        name: "FK_Trensaction_Facture_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Facture",
                        principalColumn: "FACT_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preference",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    ParkingId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preference", x => new { x.PlaceId, x.ParkingId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_Preferences_Client",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Cli_Id");
                    table.ForeignKey(
                        name: "FK_Preferences_Parkings",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "PARK_Id");
                    table.ForeignKey(
                        name: "FK_Preferences_Places",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PLA_Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    RES_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RES_DateReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RES_DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RES_DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RES_DureeTotal_Initiale = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    RES_DureeTotal_Reele = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    RES_Cloture = table.Column<bool>(type: "bit", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    PlaceParkingPLA_Id = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    FactureId = table.Column<int>(type: "int", nullable: true),
                    FormPrixId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.RES_Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Facture_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Facture",
                        principalColumn: "FACT_Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Places_PlaceParkingPLA_Id",
                        column: x => x.PlaceParkingPLA_Id,
                        principalTable: "Places",
                        principalColumn: "PLA_Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Client",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Cli_Id");
                    table.ForeignKey(
                        name: "FK_Reservations_FormulesPrix",
                        column: x => x.FormPrixId,
                        principalTable: "FormuleDePrix",
                        principalColumn: "FORM_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_PaysId",
                table: "Adresses",
                column: "PaysId");

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_UtilisateurId",
                table: "CompteBank",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeWorkOn_ParkingId",
                table: "EmployeWorkOn",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_FormuleDePrix_TypeId",
                table: "FormuleDePrix",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Formules_ParkingId",
                table: "FormuleDePrix",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_AdreseId",
                table: "Parkings",
                column: "AdreseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pays_PAYS_Nom",
                table: "Pays",
                column: "PAYS_Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personne_PERS_Email",
                table: "Personne",
                column: "PERS_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_ParkingId",
                table: "Places",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Preference_ClientId",
                table: "Preference",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Preference_ParkingId",
                table: "Preference",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FormPrixId",
                table: "Reservation",
                column: "FormPrixId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PlaceParkingPLA_Id",
                table: "Reservation",
                column: "PlaceParkingPLA_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FactureId",
                table: "Reservation",
                column: "FactureId",
                unique: true,
                filter: "[FactureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UtilisateurId",
                table: "Reservation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FactureId",
                table: "Trensaction",
                column: "FactureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trensaction_CompteEntreprise",
                table: "Trensaction",
                column: "CompteEntreprise");

            migrationBuilder.CreateIndex(
                name: "IX_Trensaction_CompteUnId",
                table: "Trensaction",
                column: "CompteUnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeWorkOn");

            migrationBuilder.DropTable(
                name: "Preference");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Trensaction");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "FormuleDePrix");

            migrationBuilder.DropTable(
                name: "CompteBank");

            migrationBuilder.DropTable(
                name: "InfoEntreprise");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "FormulesPrixType");

            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Personne");

            migrationBuilder.DropTable(
                name: "Pays");
        }
    }
}
