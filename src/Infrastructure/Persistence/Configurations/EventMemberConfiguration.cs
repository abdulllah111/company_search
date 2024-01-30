using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
   public class EventMemberConfiguration : IEntityTypeConfiguration<EventMember>
    {
        public void Configure(EntityTypeBuilder<EventMember> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}