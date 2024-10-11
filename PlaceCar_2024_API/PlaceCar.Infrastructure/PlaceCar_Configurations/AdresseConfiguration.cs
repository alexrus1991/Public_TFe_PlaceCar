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
    public class AdresseConfiguration : IEntityTypeConfiguration<Adresse>
    {
        public void Configure(EntityTypeBuilder<Adresse> builder)
        {
            builder.ToTable("Adresses");
            builder.HasKey(a => a.ADRS_Id);
            builder.Property(a => a.ADRS_Id).ValueGeneratedOnAdd();

            //builder.HasIndex(e => e.ParkingId, "IX_Adresses_ParkingId").IsUnique();

            builder.HasIndex(e => e.PaysId, "IX_Adresses_PaysId");

            //builder.HasOne(a => a.Parking).WithOne(p => p.Adresse);
            builder.HasOne(d => d.Parking).WithOne(p => p.Adresse);//.HasForeignKey<Adresse>(d => d.ParkingId);

            //builder.HasOne(a => a.Pays).WithMany(pa => pa.Adresses);


            builder.HasOne(d => d.Pays).WithMany(p => p.Adresses).HasForeignKey(d => d.PaysId);


            builder.Property(b => b.ADRS_Latitude)
                .HasPrecision(6,2);

            builder.Property(c => c.ADRS_Longitude)
                .HasPrecision(6, 2);
        }
    }
}
