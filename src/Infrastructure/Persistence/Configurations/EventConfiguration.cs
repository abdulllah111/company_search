using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasMany(e => e.EventCategories)
            .WithOne(ec => ec.Event)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Members)
            .WithOne(em => em.Event)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ChildEvents)
            .WithOne()
            .HasForeignKey(e => e.ParentEventId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}