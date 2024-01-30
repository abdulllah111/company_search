using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Queries.Events.GetEvents;
using FluentValidation;

namespace Application.Validations.Events
{
    public class GetEventsQueryValidation : AbstractValidator<GetEventsQuery>
    {
        public GetEventsQueryValidation(){
            RuleFor(getEventsQuery => getEventsQuery.UserId).NotEqual(Guid.Empty);
        }
    }
}