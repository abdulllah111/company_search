using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Categories;
using FluentValidation;

namespace Application.Validations.Categories
{
    public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidation(){
            RuleFor(createCategoryCommand => createCategoryCommand.Name).NotEmpty().MaximumLength(40).MinimumLength(2);
        }
    }
}