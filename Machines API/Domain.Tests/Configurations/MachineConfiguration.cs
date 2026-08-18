using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkMigrations.Configurations
{
    public class MachineConfiguration
        : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.ToTable("Machine");
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
