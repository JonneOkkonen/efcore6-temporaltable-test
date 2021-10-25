using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore6SimpleTest
{
    public class MainEntityConfiguration : IEntityTypeConfiguration<MainEntity>
    {
        public void Configure(EntityTypeBuilder<MainEntity> builder)
        {
            builder.ToTable("MainEntity", tb => tb.IsTemporal(ttb =>
            {
                ttb.HasPeriodStart("StartTime");
                ttb.HasPeriodEnd("EndTime");
                ttb.UseHistoryTable("ConfHistory");
            }));
            builder.Property(me => me.Id).UseIdentityColumn();
            builder.OwnsOne(me => me.OwnedEntity).WithOwner();
            builder.OwnsOne(me => me.OwnedEntity, oe =>
            {
                oe.ToTable("OwnedEntity", tb => tb.IsTemporal(ttb =>
                {
                    ttb.HasPeriodStart("StartTime");
                    ttb.HasPeriodEnd("EndTime");
                    ttb.UseHistoryTable("OwnedEntityHistory");
                }));
            });
        }
    }
}
