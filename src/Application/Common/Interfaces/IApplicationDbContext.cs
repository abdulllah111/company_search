using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Event> Events {get;}
        DbSet<User> Users {get;}
        DbSet<Category> Categories {get;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}