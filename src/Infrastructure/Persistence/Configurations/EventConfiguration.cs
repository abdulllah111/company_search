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

            builder.OwnsOne(e => e.Location);
            // builder.OwnsOne(e => e.Members);

         

            builder.HasOne(e => e.ParentEvent)
            .WithMany(e => e.ChildEvents)
            .HasForeignKey(e => e.ParentEventId);
        }
    }
}