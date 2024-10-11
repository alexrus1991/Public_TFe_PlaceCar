using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Perm_id);

            var permissions = Enum.GetValues<PermissionEnum>().Select(p => new Permission
            {
                Perm_id = (int)p,
                Perm_name = p.ToString()

            });

           
            //builder.HasMany(p => p.Roles)
            //    .WithMany(r => r.Permissions)
            //    .UsingEntity<RolePermission>(                   
            //        r => r.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
            //        l => l.HasOne<Permission>().WithMany().HasForeignKey(e => e.PermissionId));

            builder.HasData(permissions);

        }
    }
}
