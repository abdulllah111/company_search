using MediatR;

namespace Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public Guid? CreatorId { get; set; }
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public bool? IsShared { get; set; }
        public CreateCategoryCommand(){
            IsShared = false;
        }
    }
}