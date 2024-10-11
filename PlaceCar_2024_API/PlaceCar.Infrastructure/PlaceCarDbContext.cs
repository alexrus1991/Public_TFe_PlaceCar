using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlaceCar.Domain.Entities;
using PlaceCar.Infrastructure.PlaceCar_Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure
{
    public class PlaceCarDbContext : DbContext
    {
       // private readonly IOptions<AuthorizationOptions> _authOptions;
        public PlaceCarDbContext(DbContextOptions<PlaceCarDbContext> options) : base(options)
        {
            
        }

        //protected PlaceCarDbContext() : base()
        //{
        //}
        public DbSet<EmployeWorkOn> EmployeWorkOn { get; set; }
        public DbSet<PaysEntity> Pays { get; set; }
        public DbSet<Adresse> Adresse { get; set; }
        public DbSet<ParkingEntity> Parking { get; set; }
        public DbSet<FormuleDePrix> FormuleDePrix { get; set; }
        public DbSet<FormuleDePrixType> FormulesPrixType { get; set; }
        public DbSet<PlaceParking> PlaceParking { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        //public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Trensaction> Trensaction { get; set; }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<CompteBank> CompteBank { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Personne> Personne { get; set; }
        public DbSet<InfoEntreprise> InfoEntreprise { get; set; }
        public DbSet<Preferences> Preference { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }   
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<PersonneRole> PersonneRole { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new PaysConfiguration());
            modelBuilder.ApplyConfiguration(new AdresseConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingConfiguration());
            modelBuilder.ApplyConfiguration(new FormuleConfiguration());
            modelBuilder.ApplyConfiguration(new FormuleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new PersonneConfiguration());
            modelBuilder.ApplyConfiguration(new TrensactionConfiguration());   
            modelBuilder.ApplyConfiguration(new FactureConfiguration());
            modelBuilder.ApplyConfiguration(new CompteConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeWorkOnConfiguration());
            modelBuilder.ApplyConfiguration(new EmplyeeConfiguration());
            modelBuilder.ApplyConfiguration(new InfoEntrepriseConfiguration());
            modelBuilder.ApplyConfiguration(new PersonneConfiguration());
            modelBuilder.ApplyConfiguration(new PreferencesConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PersonneRoleConfiguration());




            base.OnModelCreating(modelBuilder);
           
        }
    }
}

   
