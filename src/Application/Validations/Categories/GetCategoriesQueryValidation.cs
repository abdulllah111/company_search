using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Queries.Categories.GetCategories;
using FluentValidation;

namespace Application.Validations.Categories
{
    public class GetCategoriesQueryValidation : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidation(){
            RuleFor(getCategoriesQuery => getCategoriesQuery.UserId).NotEqual(Guid.Empty);
        }
    }
}