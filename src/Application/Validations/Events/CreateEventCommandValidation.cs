using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using Domain.Enums;
using FluentValidation;

namespace Application.Validations.Events
{
    public class CreateEventCommandValidation : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidation(){
            RuleFor(createEventCommand => createEventCommand.CreatorId).NotEqual(Guid.Empty);
            RuleFor(createEventCommand => createEventCommand.Name).NotEmpty().MaximumLength(200).MinimumLength(5);
            RuleFor(createEventCommand => createEventCommand.Description).NotEmpty().MaximumLength(2000).MinimumLength(20);
            RuleFor(createEventCommand => createEventCommand.StartDate).NotEmpty().When(x => x.StartDate > DateTime.UtcNow);
            RuleFor(createEventCommand => createEventCommand.MinParticipants).NotEmpty().When(x => x.MinParticipants > 0);
            RuleFor(createEventCommand => createEventCommand.MaxParticipants).NotEmpty();
            RuleFor(createEventCommand => createEventCommand.MinAge).NotEmpty().When(x => x.MinAge > 0);
            RuleFor(createEventCommand => createEventCommand.MaxAge).NotEmpty().When(x => x.MaxAge < 100);;
            RuleFor(createEventCommand => createEventCommand.RegistrationDeadline).NotEmpty().When(x => x.RegistrationDeadline > DateTime.UtcNow);
            RuleFor(createEventCommand => createEventCommand.ParticipantsGender).NotEmpty().IsInEnum();
            RuleFor(createEventCommand => createEventCommand.EventType).NotEmpty().IsInEnum();
            RuleFor(createEventCommand => createEventCommand.EventCategories).NotEmpty();   
        }
    }
}