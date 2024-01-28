using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Common.Mappings;
using AutoMapper;

namespace WebAPI.Models
{
    public class CreateCategoryDto : IMapFrom<CreateCategoryCommand>
    {
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
            .ForMember(categoryDtp => categoryDtp.Name,
            opt => opt.MapFrom(category => category.Name));
        }
    }
}