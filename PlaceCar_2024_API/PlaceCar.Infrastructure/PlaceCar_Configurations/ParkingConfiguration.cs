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
    public class ParkingConfiguration : IEntityTypeConfiguration<ParkingEntity>
    {
        public void Configure(EntityTypeBuilder<ParkingEntity> builder)
        {
            builder.ToTable("Parkings");
            builder.HasKey(p => p.PARK_Id);
            builder.Property(p => p.PARK_Id).ValueGeneratedOnAdd();

             builder.HasOne(p => p.Adresse)
                .WithOne(a => a.Parking).HasForeignKey<ParkingEntity>(p=>p.AdreseId);

            builder
                    .HasMany<EmployeWorkOn>(d => d.EmployeWorkOn)
                    .WithOne(p => p.Parking);

            builder.HasMany(p => p.Formules)
                .WithOne(f => f.Parking);

            //builder.HasMany(p => p.Places)
            //    .WithOne(pp => pp.Parking);


        }
    }
}
