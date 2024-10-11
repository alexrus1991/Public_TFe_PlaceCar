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
    public class FormuleTypeConfiguration : IEntityTypeConfiguration<FormuleDePrixType>
    {
        public void Configure(EntityTypeBuilder<FormuleDePrixType> builder)
        {
            builder.HasKey(ft => ft.FORM_Type_Id);
            builder.Property(ft => ft.FORM_Type_Id).ValueGeneratedOnAdd();

            builder.Property(b => b.FORM_Type_Duree)
                .HasPrecision(6, 2);
            builder.HasMany(d => d.FormuleDePrix).WithOne(p => p.FormuleDePrixType);
                //.HasForeignKey(d => d.FORM_Id);
            



        }
    }
}
