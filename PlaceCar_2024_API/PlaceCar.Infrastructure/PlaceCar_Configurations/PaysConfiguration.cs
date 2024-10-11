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
    public class PaysConfiguration : IEntityTypeConfiguration<PaysEntity>
    {
        public void Configure(EntityTypeBuilder<PaysEntity> builder)
        {
            builder.HasKey(a => a.PAYS_Id);
            builder.Property(a => a.PAYS_Id).ValueGeneratedOnAdd();

            builder.HasIndex(p => p.PAYS_Nom).IsUnique();

            builder.HasMany(p => p.Adresses)
                .WithOne(a => a.Pays);

           
        }
    }
}
