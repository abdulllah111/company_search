using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using FluentValidation;

namespace Application.Validations.Events
{
    public class UpdateEventCommandValidation : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidation(){
            RuleFor(updateEventCommand => updateEventCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateEventCommand => updateEventCommand.CreatorId).NotEqual(Guid.Empty);
            RuleFor(updateEventCommand => updateEventCommand.Name).NotEmpty().MaximumLength(200);
            RuleFor(updateEventCommand => updateEventCommand.Description).NotEmpty().MaximumLength(2000);
            RuleFor(updateEventCommand => updateEventCommand.StartDate).NotEmpty().When(x => x.StartDate > DateTime.UtcNow);
            RuleFor(updateEventCommand => updateEventCommand.MinParticipants).NotEmpty().When(x => x.MinParticipants > 0);
            RuleFor(updateEventCommand => updateEventCommand.MaxParticipants).NotEmpty();
            RuleFor(updateEventCommand => updateEventCommand.MinAge).NotEmpty().When(x => x.MinAge > 0);
            RuleFor(updateEventCommand => updateEventCommand.MaxAge).NotEmpty().When(x => x.MaxAge < 100);;
            RuleFor(updateEventCommand => updateEventCommand.RegistrationDeadline).NotEmpty().When(x => x.RegistrationDeadline > DateTime.UtcNow);
            RuleFor(updateEventCommand => updateEventCommand.ParticipantsGender).NotEmpty().IsInEnum();
            RuleFor(updateEventCommand => updateEventCommand.EventType).NotEmpty().IsInEnum();
            RuleFor(updateEventCommand => updateEventCommand.EventCategories).NotEmpty();  
        }
    }
}