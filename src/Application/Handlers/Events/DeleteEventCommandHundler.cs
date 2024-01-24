using Application.Commands.Events;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Handlers.Events
{
    public class DeleteEventCommandHundler : IRequestHandler<DeleteEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEventCommandHundler(IApplicationDbContext context){
            _context = context;
        }
        public async Task<Guid> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotImplementedException();
            
            _context.Events.Remove(entity);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}