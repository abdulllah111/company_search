using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Event> Events {get;}
        DbSet<Category> Categories {get;}
        DbSet<EventMember> EventMembers {get;}


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}