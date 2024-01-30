using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Queries.Events.GetEvent;
using FluentValidation;

namespace Application.Validations.Events
{
    public class GetEventDetailsQueryValidation : AbstractValidator<GetEventDetailsQuery>
    {
        public GetEventDetailsQueryValidation(){
            RuleFor(getEventDetailsQuery => getEventDetailsQuery.Id).NotEqual(Guid.Empty);
            RuleFor(getEventDetailsQuery => getEventDetailsQuery.UserId).NotEqual(Guid.Empty);
        }
    }
}