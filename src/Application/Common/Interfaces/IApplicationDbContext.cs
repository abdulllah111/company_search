using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Event> Events {get;}
        DbSet<User> Users {get;}
        DbSet<EventCategory> EventCategories {get;}
        DbSet<EventMember> EventMembers {get;}
        DbSet<Category> Categories {get;}


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}