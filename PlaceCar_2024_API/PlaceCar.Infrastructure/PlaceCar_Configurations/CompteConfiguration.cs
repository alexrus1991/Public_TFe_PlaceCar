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
    public class CompteConfiguration : IEntityTypeConfiguration<CompteBank>
    {
        public void Configure(EntityTypeBuilder<CompteBank> builder)
        {
            builder.HasKey(a => a.CB_Id);
            builder.Property(a => a.CB_Id).ValueGeneratedOnAdd();


            builder.HasIndex(e => e.ClientId, "IX_Comptes_UtilisateurId").IsUnique();
            //builder.Property(b => b.CB_Solde).HasPrecision(4, 2);

            //builder.HasOne(c => c.Utilisateur).WithOne(u => u.CompteBank);
            builder.HasOne(d => d.Client).WithOne(p => p.Comptes)
                .HasForeignKey<CompteBank>(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comptes_Client");



        }
    }
}
