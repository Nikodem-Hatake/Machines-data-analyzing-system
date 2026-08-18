using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkMigrations.Configurations
{
    public class MachineDatasConfiguration
        : IEntityTypeConfiguration<MachineDatas>
    {
        public void Configure(EntityTypeBuilder<MachineDatas> builder)
        {
            builder.ToTable("MachineDatas");
            builder.Property(x => x.UpdateDataDate).HasMaxLength(25);
        }
    }
}
