using MediatR;

namespace Application.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}