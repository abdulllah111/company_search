using MediatR;

namespace Application.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<Guid>
    {
        public Guid CreatorId { get; set; }
        public Guid Id { get; set; }
    }
}