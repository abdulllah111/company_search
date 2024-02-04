using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace WebAPI.Models
{
    public class CreateEventDto : IMapFrom<CreateEventCommand>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public LocationPoint? Location { get; set; }
        public required EventType EventType { get; set; }
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public Gender ParticipantsGender { get; set; }
        public IList<Category>? EventCategories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEventDto, CreateEventCommand>()
            .ForMember(eventCommand => eventCommand.Name,
            opt => opt.MapFrom(eventDto => eventDto.Name))
            .ForMember(eventCommand => eventCommand.Description,
            opt => opt.MapFrom(eventDto => eventDto.Description))
            .ForMember(eventCommand => eventCommand.StartDate,
            opt => opt.MapFrom(eventDto => eventDto.StartDate))
            .ForMember(eventCommand => eventCommand.EndDate,
            opt => opt.MapFrom(eventDto => eventDto.EndDate))
            .ForMember(eventCommand => eventCommand.Location,
            opt => opt.MapFrom(eventDto => eventDto.Location))
            .ForMember(eventCommand => eventCommand.EventType,
            opt => opt.MapFrom(eventDto => eventDto.EventType))
            .ForMember(eventCommand => eventCommand.MinParticipants,
            opt => opt.MapFrom(eventDto => eventDto.MinParticipants))
            .ForMember(eventCommand => eventCommand.MaxParticipants,
            opt => opt.MapFrom(eventDto => eventDto.MaxParticipants))
            .ForMember(eventCommand => eventCommand.MinAge,
            opt => opt.MapFrom(eventDto => eventDto.MinAge))
            .ForMember(eventCommand => eventCommand.MaxAge,
            opt => opt.MapFrom(eventDto => eventDto.MaxAge))
            .ForMember(eventCommand => eventCommand.RegistrationDeadline,
            opt => opt.MapFrom(eventDto => eventDto.RegistrationDeadline))
            .ForMember(eventCommand => eventCommand.ParticipantsGender,
            opt => opt.MapFrom(eventDto => eventDto.ParticipantsGender))
            .ForMember(eventCommand => eventCommand.EventCategories,
            opt => opt.MapFrom(eventDto => eventDto.EventCategories));
        }
    }
}


