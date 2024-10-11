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
    internal class EmployeWorkOnConfiguration : IEntityTypeConfiguration<EmployeWorkOn>
    {
        public void Configure(EntityTypeBuilder<EmployeWorkOn> builder)
        {
            builder.ToTable("EmployeWorkOn");
            builder.HasKey(p => new { p.Emp_Pers_Id, p.ParkingId }).IsClustered().HasName("PK_Employe_parking");

        }
    }
}
