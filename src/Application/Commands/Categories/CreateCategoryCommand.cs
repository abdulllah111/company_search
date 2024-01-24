using MediatR;

namespace Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}