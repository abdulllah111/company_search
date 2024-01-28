using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using FluentValidation;

namespace Application.Validations.Events
{
    public class DeleteEventCommandValidation : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidation(){
            RuleFor(deleteEventCommand => deleteEventCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteEventCommand => deleteEventCommand.CreatorId).NotEqual(Guid.Empty);
        }
    }
}