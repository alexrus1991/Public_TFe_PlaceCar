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
    public class PersonneConfiguration : IEntityTypeConfiguration<Personne>
    {
        public void Configure(EntityTypeBuilder<Personne> builder)
        {
            builder.HasKey(u => u.PERS_Id);
            builder.Property(u => u.PERS_Id).ValueGeneratedOnAdd();

            builder.Property(d => d.PERS_DateNaissance)
                .HasColumnType("datetime2");

            builder.HasOne(p => p.Client)
                .WithOne(c => c.Cli);

            builder.HasOne(p => p.Employee)
                .WithOne(e => e.EmpPers);

            builder.HasIndex(p=>p.PERS_Email).IsUnique();

            //builder.HasMany(p => p.Roles)
            //    .WithMany(r => r.Personnes)
            //    .UsingEntity<PersonneRole>(
            //        l => l.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.NoAction),
            //        r => r.HasOne<Personne>().WithMany().HasForeignKey(p => p.PersonneId).OnDelete(DeleteBehavior.NoAction));
            builder
                   .HasMany<PersonneRole>(d => d.PersonneRoles)
                   .WithOne(p => p.Personne);

            //builder.HasMany(u => u.Reservations)
            //    .WithOne(r => r.Utilisateur);

            //builder.HasOne(u => u.CompteBank)
            //    .WithOne(cb => cb.Utilisateur);

            //builder.HasOne(u => u.Parking)
            //    .WithMany(p => p.Utilisateurs);


        }
    }
}
