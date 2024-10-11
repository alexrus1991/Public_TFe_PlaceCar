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
    public class FormuleConfiguration : IEntityTypeConfiguration<FormuleDePrix>
    {
        public void Configure(EntityTypeBuilder<FormuleDePrix> builder)
        {
            builder.HasKey(a => a.FORM_Id);
            builder.Property(a => a.FORM_Id).ValueGeneratedOnAdd();

            builder.HasIndex(e => e.ParkingId, "IX_Formules_ParkingId");

            builder.HasOne(d => d.Parking).WithMany(p => p.Formules).HasForeignKey(d => d.ParkingId);

            builder.Property(b => b.FORM_Prix)
                .HasPrecision(6, 2);
            builder.HasOne(a => a.FormuleDePrixType).WithMany(ft => ft.FormuleDePrix).HasForeignKey(d => d.TypeId);


            //builder.HasOne(a => a.Parking)
            //    .WithMany(p => p.Formules);






        }
    }
}
