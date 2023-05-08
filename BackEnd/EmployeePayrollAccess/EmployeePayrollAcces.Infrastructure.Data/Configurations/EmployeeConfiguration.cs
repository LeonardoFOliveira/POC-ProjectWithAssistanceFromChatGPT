using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmployeePayrollAccess.Domain.Entities;

namespace EmployeePayrollAccess.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(e => e.Cpf).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}
