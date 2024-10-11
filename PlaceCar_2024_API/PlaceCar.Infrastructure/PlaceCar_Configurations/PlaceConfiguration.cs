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
    public class PlaceConfiguration : IEntityTypeConfiguration<PlaceParking>
    {
        public void Configure(EntityTypeBuilder<PlaceParking> builder)
        {
            builder.ToTable("Places");
            builder.HasKey(pp => pp.PLA_Id);
            //builder.Property(pp => pp.PLA_Id).ValueGeneratedOnAdd();
            builder.HasIndex(e => e.ParkingId, "IX_Places_ParkingId");


            builder.HasOne(d => d.Parking).WithMany(p => p.Places).HasForeignKey(d => d.ParkingId);

            builder.HasMany(p => p.Reservation).WithOne(r => r.PlaceParking).HasForeignKey(d=>d.PlaceId);


            //builder.HasOne(pp => pp.Parking)
            //    .WithMany(p => p.Places);

            //builder.HasOne(pp => pp.Reservation)
            //    .WithOne(r => r.PlaceParking)
            //    .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
