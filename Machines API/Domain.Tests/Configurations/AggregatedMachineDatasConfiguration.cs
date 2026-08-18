using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkMigrations.Configurations
{
    public class AggregatedMachineDatasConfiguration
        : IEntityTypeConfiguration<AggregatedMachineDatas>
    {
        public void Configure(EntityTypeBuilder<AggregatedMachineDatas> builder)
        {
            builder.ToTable("AggregatedMachineDatas");
            builder.Property(x => x.StartDate).HasMaxLength(20);
        }
    }
}
