﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlaceCar.Infrastructure;

#nullable disable

namespace PlaceCar.Infrastructure.Migrations
{
    [DbContext(typeof(PlaceCarDbContext))]
    [Migration("20240426104640_Modification entity personneRole")]
    partial class ModificationentitypersonneRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PlaceCar.Domain.Entities.Adresse", b =>
                {
                    b.Property<int>("ADRS_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ADRS_Id"));

                    b.Property<decimal>("ADRS_Latitude")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<decimal>("ADRS_Longitude")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("ADRS_NomRue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ADRS_Numero")
                        .HasColumnType("int");

                    b.Property<string>("ADRS_Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaysId")
                        .HasColumnType("int");

                    b.HasKey("ADRS_Id");

                    b.HasIndex(new[] { "PaysId" }, "IX_Adresses_PaysId");

                    b.ToTable("Adresses", (string)null);
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Client", b =>
                {
                    b.Property<int>("Cli_Id")
                        .HasColumnType("int");

                    b.HasKey("Cli_Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.CompteBank", b =>
                {
                    b.Property<int>("CB_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CB_Id"));

                    b.Property<DateTime>("CB_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("CB_Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CB_NumCompte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("CB_Id");

                    b.HasIndex(new[] { "ClientId" }, "IX_Comptes_UtilisateurId")
                        .IsUnique();

                    b.ToTable("CompteBank");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.EmployeWorkOn", b =>
                {
                    b.Property<int>("Emp_Pers_Id")
                        .HasColumnType("int");

                    b.Property<int>("ParkingId")
                        .HasColumnType("int");

                    b.HasKey("Emp_Pers_Id", "ParkingId")
                        .HasName("PK_Employe_parking");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Emp_Pers_Id", "ParkingId"));

                    b.HasIndex("ParkingId");

                    b.ToTable("EmployeWorkOn", (string)null);
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Emp_Pers_Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.HasKey("Emp_Pers_Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Facture", b =>
                {
                    b.Property<int>("FACT_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FACT_Id"));

                    b.Property<DateTime>("FACT_Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FACT_Somme")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("FACT_Id");

                    b.ToTable("Facture");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.FormuleDePrix", b =>
                {
                    b.Property<int>("FORM_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FORM_Id"));

                    b.Property<decimal>("FORM_Prix")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<int>("ParkingId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("FORM_Id");

                    b.HasIndex("TypeId");

                    b.HasIndex(new[] { "ParkingId" }, "IX_Formules_ParkingId");

                    b.ToTable("FormuleDePrix");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.FormuleDePrixType", b =>
                {
                    b.Property<int>("FORM_Type_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FORM_Type_Id"));

                    b.Property<string>("FORM_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FORM_Type_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FORM_Type_Id");

                    b.ToTable("FormulesPrixType");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.InfoEntreprise", b =>
                {
                    b.Property<string>("Nom")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Cb_NumCompte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nom");

                    b.ToTable("InfoEntreprise");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.ParkingEntity", b =>
                {
                    b.Property<int>("PARK_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PARK_Id"));

                    b.Property<int>("AdreseId")
                        .HasColumnType("int");

                    b.Property<int>("PARK_NbEtages")
                        .HasColumnType("int");

                    b.Property<int>("PARK_NbPlaces")
                        .HasColumnType("int");

                    b.Property<string>("PARK_Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PARK_Id");

                    b.HasIndex("AdreseId")
                        .IsUnique();

                    b.ToTable("Parkings", (string)null);
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PaysEntity", b =>
                {
                    b.Property<int>("PAYS_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PAYS_Id"));

                    b.Property<string>("PAYS_Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PAYS_Id");

                    b.HasIndex("PAYS_Nom")
                        .IsUnique();

                    b.ToTable("Pays");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Perm_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Perm_id"));

                    b.Property<string>("Perm_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Perm_id");

                    b.ToTable("Permission");

                    b.HasData(
                        new
                        {
                            Perm_id = 1,
                            Perm_name = "read_admin"
                        },
                        new
                        {
                            Perm_id = 2,
                            Perm_name = "write_admin"
                        },
                        new
                        {
                            Perm_id = 3,
                            Perm_name = "read_parking"
                        },
                        new
                        {
                            Perm_id = 4,
                            Perm_name = "write_parking"
                        },
                        new
                        {
                            Perm_id = 5,
                            Perm_name = "read_all"
                        },
                        new
                        {
                            Perm_id = 6,
                            Perm_name = "write_all"
                        });
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Personne", b =>
                {
                    b.Property<int>("PERS_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PERS_Id"));

                    b.Property<DateTime>("PERS_DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("PERS_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PERS_Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PERS_Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PERS_Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PERS_Id");

                    b.HasIndex("PERS_Email")
                        .IsUnique();

                    b.ToTable("Personne");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PersonneRole", b =>
                {
                    b.Property<int>("PersonneId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PersonnePERS_Id")
                        .HasColumnType("int");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("PersonneId", "RoleId");

                    b.HasIndex("PersonnePERS_Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Role_Id");

                    b.ToTable("PersonneRole");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PlaceParking", b =>
                {
                    b.Property<int>("PLA_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PLA_Id"));

                    b.Property<int>("PLA_Etage")
                        .HasColumnType("int");

                    b.Property<int>("PLA_NumeroPlace")
                        .HasColumnType("int");

                    b.Property<int>("ParkingId")
                        .HasColumnType("int");

                    b.HasKey("PLA_Id");

                    b.HasIndex(new[] { "ParkingId" }, "IX_Places_ParkingId");

                    b.ToTable("Places", (string)null);
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Preferences", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<int>("ParkingId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("PlaceId", "ParkingId", "ClientId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ParkingId");

                    b.ToTable("Preference");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("RES_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RES_Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("FactureId")
                        .HasColumnType("int");

                    b.Property<int>("FormPrixId")
                        .HasColumnType("int");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<bool>("RES_Cloture")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RES_DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RES_DateFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RES_DateReservation")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RES_DureeTotal_Initiale")
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)");

                    b.Property<decimal>("RES_DureeTotal_Reele")
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)");

                    b.HasKey("RES_Id");

                    b.HasIndex("FormPrixId");

                    b.HasIndex("PlaceId");

                    b.HasIndex(new[] { "FactureId" }, "IX_Reservations_FactureId")
                        .IsUnique()
                        .HasFilter("[FactureId] IS NOT NULL");

                    b.HasIndex(new[] { "ClientId" }, "IX_Reservations_UtilisateurId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Role_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Role_Id"));

                    b.Property<string>("Role_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Role_Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Role_Id = 1,
                            Role_Name = "Admin"
                        },
                        new
                        {
                            Role_Id = 2,
                            Role_Name = "Employee"
                        },
                        new
                        {
                            Role_Id = 3,
                            Role_Name = "Client"
                        });
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.RolePermission", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionPerm_id")
                        .HasColumnType("int");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("PermissionPerm_id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Role_Id");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Trensaction", b =>
                {
                    b.Property<int>("TRANS_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TRANS_Id"));

                    b.Property<string>("CompteEntreprise")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("CompteUnId")
                        .HasColumnType("int");

                    b.Property<int>("FactureId")
                        .HasColumnType("int");

                    b.Property<string>("TRANS_Communication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TRANS_Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TRANS_Somme")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("TRANS_Id");

                    b.HasIndex("CompteEntreprise");

                    b.HasIndex("CompteUnId");

                    b.HasIndex(new[] { "FactureId" }, "IX_Transactions_FactureId")
                        .IsUnique();

                    b.ToTable("Trensaction");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Adresse", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.PaysEntity", "Pays")
                        .WithMany("Adresses")
                        .HasForeignKey("PaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pays");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Client", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Personne", "Cli")
                        .WithOne("Client")
                        .HasForeignKey("PlaceCar.Domain.Entities.Client", "Cli_Id")
                        .IsRequired()
                        .HasConstraintName("FK_Client_Personne");

                    b.Navigation("Cli");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.CompteBank", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Client", "Client")
                        .WithOne("Comptes")
                        .HasForeignKey("PlaceCar.Domain.Entities.CompteBank", "ClientId")
                        .IsRequired()
                        .HasConstraintName("FK_Comptes_Client");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.EmployeWorkOn", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Employee", "Employee")
                        .WithMany("EmployeWorkOns")
                        .HasForeignKey("Emp_Pers_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.ParkingEntity", "Parking")
                        .WithMany("EmployeWorkOn")
                        .HasForeignKey("ParkingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Parking");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Employee", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Personne", "EmpPers")
                        .WithOne("Employee")
                        .HasForeignKey("PlaceCar.Domain.Entities.Employee", "Emp_Pers_Id")
                        .IsRequired()
                        .HasConstraintName("FK_Employee_Personne");

                    b.Navigation("EmpPers");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.FormuleDePrix", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.ParkingEntity", "Parking")
                        .WithMany("Formules")
                        .HasForeignKey("ParkingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.FormuleDePrixType", "FormuleDePrixType")
                        .WithMany("FormuleDePrix")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormuleDePrixType");

                    b.Navigation("Parking");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.ParkingEntity", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Adresse", "Adresse")
                        .WithOne("Parking")
                        .HasForeignKey("PlaceCar.Domain.Entities.ParkingEntity", "AdreseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adresse");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PersonneRole", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Personne", null)
                        .WithMany()
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("PersonnePERS_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personne");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PlaceParking", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.ParkingEntity", "Parking")
                        .WithMany("Places")
                        .HasForeignKey("ParkingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parking");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Preferences", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Client", "Client")
                        .WithMany("Preferences")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK_Preferences_Client");

                    b.HasOne("PlaceCar.Domain.Entities.ParkingEntity", "Parking")
                        .WithMany("Preferences")
                        .HasForeignKey("ParkingId")
                        .IsRequired()
                        .HasConstraintName("FK_Preferences_Parkings");

                    b.HasOne("PlaceCar.Domain.Entities.PlaceParking", "Place")
                        .WithMany("Preferences")
                        .HasForeignKey("PlaceId")
                        .IsRequired()
                        .HasConstraintName("FK_Preferences_Places");

                    b.Navigation("Client");

                    b.Navigation("Parking");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK_Reservations_Client");

                    b.HasOne("PlaceCar.Domain.Entities.Facture", "Facture")
                        .WithOne("Reservation")
                        .HasForeignKey("PlaceCar.Domain.Entities.Reservation", "FactureId");

                    b.HasOne("PlaceCar.Domain.Entities.FormuleDePrix", "ResFormPrixNavigation")
                        .WithMany("Reservations")
                        .HasForeignKey("FormPrixId")
                        .IsRequired()
                        .HasConstraintName("FK_Reservations_FormulesPrix");

                    b.HasOne("PlaceCar.Domain.Entities.PlaceParking", "PlaceParking")
                        .WithMany("Reservation")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Facture");

                    b.Navigation("PlaceParking");

                    b.Navigation("ResFormPrixNavigation");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionPerm_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PlaceCar.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Trensaction", b =>
                {
                    b.HasOne("PlaceCar.Domain.Entities.InfoEntreprise", "CompteEntrepriseNavigation")
                        .WithMany("Transactions")
                        .HasForeignKey("CompteEntreprise")
                        .IsRequired()
                        .HasConstraintName("FK_Transactions_InfoEntreprise");

                    b.HasOne("PlaceCar.Domain.Entities.CompteBank", "CompteUn")
                        .WithMany("Transactions")
                        .HasForeignKey("CompteUnId")
                        .IsRequired()
                        .HasConstraintName("FK_Transactions_Comptes");

                    b.HasOne("PlaceCar.Domain.Entities.Facture", "Facture")
                        .WithOne("Trensaction")
                        .HasForeignKey("PlaceCar.Domain.Entities.Trensaction", "FactureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompteEntrepriseNavigation");

                    b.Navigation("CompteUn");

                    b.Navigation("Facture");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Adresse", b =>
                {
                    b.Navigation("Parking");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Client", b =>
                {
                    b.Navigation("Comptes");

                    b.Navigation("Preferences");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.CompteBank", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Employee", b =>
                {
                    b.Navigation("EmployeWorkOns");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Facture", b =>
                {
                    b.Navigation("Reservation");

                    b.Navigation("Trensaction");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.FormuleDePrix", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.FormuleDePrixType", b =>
                {
                    b.Navigation("FormuleDePrix");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.InfoEntreprise", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.ParkingEntity", b =>
                {
                    b.Navigation("EmployeWorkOn");

                    b.Navigation("Formules");

                    b.Navigation("Places");

                    b.Navigation("Preferences");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PaysEntity", b =>
                {
                    b.Navigation("Adresses");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.Personne", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PlaceCar.Domain.Entities.PlaceParking", b =>
                {
                    b.Navigation("Preferences");

                    b.Navigation("Reservation");
                });
#pragma warning restore 612, 618
        }
    }
}
