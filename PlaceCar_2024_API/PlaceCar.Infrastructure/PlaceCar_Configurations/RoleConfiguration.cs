using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Role_Id);

            //builder.HasMany(r => r.PermissionRoles)
            //    .WithMany(p => p.RolePermissions)
            //    .UsingEntity<RolePermission>(
            //        l => l.HasOne<Permission>().WithMany().HasForeignKey(e => e.PermissionId).OnDelete(DeleteBehavior.NoAction),
            //        r => r.HasOne<Role>().WithMany().HasForeignKey(e =>e.RoleId).OnDelete(DeleteBehavior.NoAction));
            builder
                  .HasMany<RolePermission>(d => d.PermissionRoles)
                  .WithOne(p => p.Role);

            builder
                 .HasMany<PersonneRole>(d => d.PersonnesRoles)
                 .WithOne(p => p.Role);


            var roles = Enum.GetValues<RoleEnum>().Select(r => new Role
            {
                Role_Id = (int)r,
                Role_Name = r.ToString()
            });

            builder.HasData(roles);
        }
    }
}
