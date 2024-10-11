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
    public class EmplyeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Emp_Pers_Id);

            builder.Property(e => e.Emp_Pers_Id)
                .ValueGeneratedNever();

            builder.HasOne(d => d.EmpPers).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.Emp_Pers_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Personne");

            builder
                    .HasMany<EmployeWorkOn>(d => d.EmployeWorkOns)
                    .WithOne(p => p.Employee);
            //"EmployeWorkOn",
            //        r => r.HasOne(typeof(ParkingEntity)).WithMany()
            //            .HasForeignKey("ParkingId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("FK_EmployeWorkOn_Parkings"),
            //        l => l.HasOne(typeof(Employee)).WithMany()
            //            .HasForeignKey("Emp_Pers_Id")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("FK_EmployeWorkOn_Employee"),
            //        j =>
            //        {
            //            j.HasKey("Emp_Pers_Id", "ParkingId");
            //            j.IndexerProperty<int>("Emp_Pers_Id").HasColumnName("Emp_Pers_Id");
            //        }
        }
    }
}
