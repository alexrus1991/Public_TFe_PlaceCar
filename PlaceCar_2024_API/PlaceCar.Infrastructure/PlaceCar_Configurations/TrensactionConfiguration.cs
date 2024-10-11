using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceCar.Domain.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PlaceCar.Infrastructure.PlaceCar_Configurations
{
    public class TrensactionConfiguration : IEntityTypeConfiguration<Trensaction>
    {
        public void Configure(EntityTypeBuilder<Trensaction> builder)
        {
            builder.HasKey(t => t.TRANS_Id);
            builder.Property(t => t.TRANS_Id).ValueGeneratedOnAdd();

            builder.HasIndex(e => e.FactureId, "IX_Transactions_FactureId").IsUnique();

            builder.Property(e => e.CompteEntreprise).HasMaxLength(250);

            builder.Property(b => b.TRANS_Somme)
                .HasPrecision(8, 2);

            builder.Property(d => d.TRANS_Date)
                .HasColumnType("datetime2");


            builder.HasOne(d => d.CompteEntrepriseNavigation).WithMany(p => p.Transactions)
              .HasForeignKey(d => d.CompteEntreprise)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Transactions_InfoEntreprise");

            builder.HasOne(d => d.CompteUn).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CompteUnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Comptes");

            builder.HasOne(d => d.Facture).WithOne(p => p.Trensaction).HasForeignKey<Trensaction>(d => d.FactureId);


            //builder.HasOne(t => t.Facture)
            //    .WithOne(f => f.Trensaction)
            //    .HasForeignKey<Facture>(f => f.TransactionId);

           
        }
    }
}
