using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TreeStructureAPI.Models.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasOne(i => i.ParentItem)
            .WithMany(i => i.ChildItems)
            .HasForeignKey(i => i.ParentItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}