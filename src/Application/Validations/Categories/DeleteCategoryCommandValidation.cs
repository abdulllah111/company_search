using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Categories;
using FluentValidation;

namespace Application.Validations.Categories
{
    public class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidation(){
            RuleFor(deleteCategoryCommand => deleteCategoryCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteCategoryCommand => deleteCategoryCommand.CreatorId).NotEqual(Guid.Empty);
        }
    }
}