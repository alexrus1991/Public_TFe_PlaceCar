using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceCar.Domain.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(u => u.RES_Id);
            builder.Property(u => u.RES_Id).ValueGeneratedOnAdd();

            builder.HasIndex(e => e.FactureId, "IX_Reservations_FactureId").IsUnique();

            builder.HasIndex(e => e.ClientId, "IX_Reservations_UtilisateurId");

            builder.Property(d1 => d1.RES_DateReservation)
                .HasColumnType("datetime2");
            builder.Property(d2 => d2.RES_DateDebut)
                .HasColumnType("datetime2");
            builder.Property(d3 => d3.RES_DateFin)
                .HasColumnType("datetime2");

            builder.Property(i => i.RES_DureeTotal_Initiale)
                .HasPrecision(5, 2);
            builder.Property(r => r.RES_DureeTotal_Reele)
               .HasPrecision(5, 2);

            builder.HasOne(d => d.Client).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Client");

            builder.HasOne(d => d.Facture).WithOne(p => p.Reservation).HasForeignKey<Reservation>(d => d.FactureId);

            builder.HasOne(d => d.ResFormPrixNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.FormPrixId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_FormulesPrix");


            //builder.HasOne(r => r.PlaceParking)
            //    .WithOne(pp => pp.Reservation);
            //.HasForeignKey<Reservation>(r => r.PlaceId);

            //builder.HasOne(r => r.Utilisateur)
            //    .WithMany(u => u.Reservations);

            //builder.HasOne(r => r.Facture)
            //    .WithOne(f => f.Reservation);
        }
    }
}
