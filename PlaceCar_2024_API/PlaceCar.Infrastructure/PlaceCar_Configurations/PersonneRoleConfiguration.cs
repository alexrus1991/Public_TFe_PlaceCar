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
    public class PersonneRoleConfiguration : IEntityTypeConfiguration<PersonneRole>
    {
        public void Configure(EntityTypeBuilder<PersonneRole> builder)
        {
            builder.ToTable("PersonneRole");
            builder.HasKey(p => new { p.PersonneId, p.RoleId }).IsClustered().HasName("PK_PersonneRole");
        }
    }
}
