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
    public class FactureConfiguration : IEntityTypeConfiguration<Facture>
    {
        public void Configure(EntityTypeBuilder<Facture> builder)
        {
            builder.HasKey(f => f.FACT_Id);
            builder.Property(f => f.FACT_Id).ValueGeneratedOnAdd();

           
            builder.Property(b => b.FACT_Somme)
                .HasPrecision(6, 2);

            builder.HasOne(f => f.Reservation)
                .WithOne(r => r.Facture)
                .HasForeignKey<Reservation>(r => r.FactureId);

            //builder.HasOne(f => f.Trensaction)
            //    .WithOne(t => t.Facture)
            //    .HasForeignKey<Trensaction>(f => f.FactureId);
        }
    }
}
