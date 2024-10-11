using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlaceCar.Infrastructure.output;

public partial class PlaceCarTestContext2 : DbContext
{
    public PlaceCarTestContext2()
    {
    }

    public PlaceCarTestContext2(DbContextOptions<PlaceCarTestContext2> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresses> Adresses { get; set; }

    public virtual DbSet<Client> Client { get; set; }

    public virtual DbSet<Comptes> Comptes { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<Factures> Factures { get; set; }

    public virtual DbSet<Formules> Formules { get; set; }

    public virtual DbSet<FormulesType> FormulesType { get; set; }

    public virtual DbSet<InfoEntreprise> InfoEntreprise { get; set; }

    public virtual DbSet<Parkings> Parkings { get; set; }

    public virtual DbSet<Pays> Pays { get; set; }

    public virtual DbSet<Personne> Personne { get; set; }

    public virtual DbSet<Places> Places { get; set; }

    public virtual DbSet<Preferences> Preferences { get; set; }

    public virtual DbSet<Reservations> Reservations { get; set; }

    public virtual DbSet<Transactions> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-IVPHLBJ0\\SQLINSTANCE_1;Initial Catalog=PlaceCar_Test;User Id=sa;Password=ASP2022;;Encrypt=False;Trust Server Certificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresses>(entity =>
        {
            entity.HasKey(e => e.AdrsId);

            entity.HasIndex(e => e.ParkingId, "IX_Adresses_ParkingId").IsUnique();

            entity.HasIndex(e => e.PaysId, "IX_Adresses_PaysId");

            entity.Property(e => e.AdrsId).HasColumnName("ADRS_Id");
            entity.Property(e => e.AdrsLatitude)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("ADRS_Latitude");
            entity.Property(e => e.AdrsLongitude)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("ADRS_Longitude");
            entity.Property(e => e.AdrsNomRue).HasColumnName("ADRS_NomRue");
            entity.Property(e => e.AdrsNumero).HasColumnName("ADRS_Numero");
            entity.Property(e => e.AdrsVille).HasColumnName("ADRS_Ville");

            entity.HasOne(d => d.Parking).WithOne(p => p.Adresses).HasForeignKey<Adresses>(d => d.ParkingId);

            entity.HasOne(d => d.Pays).WithMany(p => p.Adresses).HasForeignKey(d => d.PaysId);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.CliId);

            entity.Property(e => e.CliId)
                .ValueGeneratedNever()
                .HasColumnName("CLI_id");

            entity.HasOne(d => d.Cli).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.CliId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_Personne");
        });

        modelBuilder.Entity<Comptes>(entity =>
        {
            entity.HasKey(e => e.CbId);

            entity.HasIndex(e => e.ClientId, "IX_Comptes_UtilisateurId").IsUnique();

            entity.Property(e => e.CbId).HasColumnName("CB_Id");
            entity.Property(e => e.CbDate).HasColumnName("CB_Date");
            entity.Property(e => e.CbNom).HasColumnName("CB_Nom");
            entity.Property(e => e.CbNumCompte).HasColumnName("CB_NumCompte");

            entity.HasOne(d => d.Client).WithOne(p => p.Comptes)
                .HasForeignKey<Comptes>(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comptes_Client");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpUtilId);

            entity.Property(e => e.EmpUtilId)
                .ValueGeneratedNever()
                .HasColumnName("EMP_UTIL_Id");

            entity.HasOne(d => d.EmpUtil).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.EmpUtilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Personne");

            entity.HasMany(d => d.Parking).WithMany(p => p.EmpUtil)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeWorkOn",
                    r => r.HasOne<Parkings>().WithMany()
                        .HasForeignKey("ParkingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeWorkOn_Parkings"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmpUtilId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeWorkOn_Employee"),
                    j =>
                    {
                        j.HasKey("EmpUtilId", "ParkingId");
                        j.IndexerProperty<int>("EmpUtilId").HasColumnName("EMP_UTIL_Id");
                    });
        });

        modelBuilder.Entity<Factures>(entity =>
        {
            entity.HasKey(e => e.FactId);

            entity.Property(e => e.FactId).HasColumnName("FACT_Id");
            entity.Property(e => e.FactDate).HasColumnName("FACT_Date");
            entity.Property(e => e.FactSomme)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("FACT_Somme");
        });

        modelBuilder.Entity<Formules>(entity =>
        {
            entity.HasKey(e => e.FormId);

            entity.HasIndex(e => e.ParkingId, "IX_Formules_ParkingId");

            entity.Property(e => e.FormId).HasColumnName("FORM_Id");
            entity.Property(e => e.FormPrix)
                .HasColumnType("decimal(2, 2)")
                .HasColumnName("FORM_Prix");

            entity.HasOne(d => d.Parking).WithMany(p => p.Formules).HasForeignKey(d => d.ParkingId);
        });

        modelBuilder.Entity<FormulesType>(entity =>
        {
            entity.HasKey(e => e.FormTypeId);

            entity.HasIndex(e => e.FormuleId, "IX_FormulesType_FormuleId").IsUnique();

            entity.Property(e => e.FormTypeId).HasColumnName("FORM_Type_Id");
            entity.Property(e => e.FormTitle).HasColumnName("FORM_Title");
            entity.Property(e => e.FormTypeDescription).HasColumnName("FORM_Type_Description");

            entity.HasOne(d => d.Formule).WithOne(p => p.FormulesType).HasForeignKey<FormulesType>(d => d.FormuleId);
        });

        modelBuilder.Entity<InfoEntreprise>(entity =>
        {
            entity.HasKey(e => e.Nom);

            entity.Property(e => e.Nom).HasMaxLength(250);
            entity.Property(e => e.CbNumCompte).HasColumnName("CB_NumCompte");
        });

        modelBuilder.Entity<Parkings>(entity =>
        {
            entity.HasKey(e => e.ParkId);

            entity.Property(e => e.ParkId).HasColumnName("PARK_Id");
            entity.Property(e => e.ParkNbEtages).HasColumnName("PARK_NbEtages");
            entity.Property(e => e.ParkNbPlaces).HasColumnName("PARK_NbPlaces");
            entity.Property(e => e.ParkNom).HasColumnName("PARK_Nom");
        });

        modelBuilder.Entity<Pays>(entity =>
        {
            entity.Property(e => e.PaysId).HasColumnName("PAYS_Id");
            entity.Property(e => e.PaysNom).HasColumnName("PAYS_Nom");
        });

        modelBuilder.Entity<Personne>(entity =>
        {
            entity.HasKey(e => e.UtilId).HasName("PK_Utilisateurs");

            entity.Property(e => e.UtilId).HasColumnName("UTIL_Id");
            entity.Property(e => e.UtilDateNaissance).HasColumnName("UTIL_DateNaissance");
            entity.Property(e => e.UtilEmail).HasColumnName("UTIL_Email");
            entity.Property(e => e.UtilNom).HasColumnName("UTIL_Nom");
            entity.Property(e => e.UtilPassword).HasColumnName("UTIL_Password");
            entity.Property(e => e.UtilPrenom).HasColumnName("UTIL_Prenom");
            entity.Property(e => e.UtilTypeUtilisateur).HasColumnName("UTIL_TypeUtilisateur");
        });

        modelBuilder.Entity<Places>(entity =>
        {
            entity.HasKey(e => e.PlaId);

            entity.HasIndex(e => e.ParkingId, "IX_Places_ParkingId");

            entity.HasIndex(e => e.ReservationId, "IX_Places_ReservationId").IsUnique();

            entity.Property(e => e.PlaId).HasColumnName("PLA_Id");
            entity.Property(e => e.PlaEstLibre).HasColumnName("PLA_EstLibre");
            entity.Property(e => e.PlaEtage).HasColumnName("PLA_Etage");
            entity.Property(e => e.PlaNumeroPlace).HasColumnName("PLA_NumeroPlace");

            entity.HasOne(d => d.Parking).WithMany(p => p.Places).HasForeignKey(d => d.ParkingId);

            entity.HasOne(d => d.Reservation).WithOne(p => p.Places)
                .HasForeignKey<Places>(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Preferences>(entity =>
        {
            entity.HasKey(e => new { e.PlaId, e.ParkId, e.CliId });

            entity.Property(e => e.PlaId).HasColumnName("PLA_Id");
            entity.Property(e => e.ParkId).HasColumnName("PARK_id");
            entity.Property(e => e.CliId).HasColumnName("CLI_id");

            entity.HasOne(d => d.Cli).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.CliId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preferences_Client");

            entity.HasOne(d => d.Park).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.ParkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preferences_Parkings");

            entity.HasOne(d => d.Pla).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.PlaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preferences_Places");
        });

        modelBuilder.Entity<Reservations>(entity =>
        {
            entity.HasKey(e => e.ResId);

            entity.HasIndex(e => e.FactureId, "IX_Reservations_FactureId").IsUnique();

            entity.HasIndex(e => e.ClientId, "IX_Reservations_UtilisateurId");

            entity.Property(e => e.ResId).HasColumnName("RES_Id");
            entity.Property(e => e.ResCloture).HasColumnName("RES_Cloture");
            entity.Property(e => e.ResDateDebut).HasColumnName("RES_DateDebut");
            entity.Property(e => e.ResDateFin).HasColumnName("RES_DateFin");
            entity.Property(e => e.ResDateReservation).HasColumnName("RES_DateReservation");
            entity.Property(e => e.ResDureeTotalInitiale)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("RES_DureeTotal_Initiale");
            entity.Property(e => e.ResDureeTotalReele)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("RES_DureeTotal_Reele");
            entity.Property(e => e.ResFormType).HasColumnName("RES_FormType");

            entity.HasOne(d => d.Client).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Client");

            entity.HasOne(d => d.Facture).WithOne(p => p.Reservations).HasForeignKey<Reservations>(d => d.FactureId);

            entity.HasOne(d => d.ResFormTypeNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ResFormType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_FormulesType");
        });

        modelBuilder.Entity<Transactions>(entity =>
        {
            entity.HasKey(e => e.TransId);

            entity.HasIndex(e => e.FactureId, "IX_Transactions_FactureId").IsUnique();

            entity.Property(e => e.TransId).HasColumnName("TRANS_Id");
            entity.Property(e => e.CompteEntreprise).HasMaxLength(250);
            entity.Property(e => e.CompteUnId).HasColumnName("CompteUn_id");
            entity.Property(e => e.TransCommunication).HasColumnName("TRANS_Communication");
            entity.Property(e => e.TransDate).HasColumnName("TRANS_Date");
            entity.Property(e => e.TransSomme)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("TRANS_Somme");

            entity.HasOne(d => d.CompteEntrepriseNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CompteEntreprise)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_InfoEntreprise");

            entity.HasOne(d => d.CompteUn).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CompteUnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Comptes");

            entity.HasOne(d => d.Facture).WithOne(p => p.Transactions).HasForeignKey<Transactions>(d => d.FactureId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
