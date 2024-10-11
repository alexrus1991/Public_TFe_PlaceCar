using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {


        public void Configure(EntityTypeBuilder<RolePermission> builder)
        { 
            builder.ToTable("RolePermission");
            builder.HasKey(p => new { p.RoleId, p.PermissionId }).IsClustered().HasName("PK_RolePermission");

        }

       


    }
}
