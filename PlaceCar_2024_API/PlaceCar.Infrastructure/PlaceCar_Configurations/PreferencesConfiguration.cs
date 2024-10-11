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
    public class PreferencesConfiguration : IEntityTypeConfiguration<Preferences>
    {
        public void Configure(EntityTypeBuilder<Preferences> builder)
        {
            builder.HasKey(e => new { e.PlaceId, e.ParkingId, e.ClientId });

            builder.HasOne(d => d.Client).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preferences_Client");

            builder.HasOne(d => d.Parking).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.ParkingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preferences_Parkings");

            builder.HasOne(d => d.Place).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preferences_Places");
        }
    }
}
