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
    public class InfoEntrepriseConfiguration : IEntityTypeConfiguration<InfoEntreprise>
    {
        public void Configure(EntityTypeBuilder<InfoEntreprise> builder)
        {
            builder.HasKey(e => e.Nom);

            builder.Property(e => e.Nom).HasMaxLength(250);

           // builder.HasOne(i=>i.Cb_NumCompte)
        }
    }
}
