using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Queries.Categories.GetCategories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public Guid? CreatorId { get; set; }
        public required string Name { get; set; }
        public Category? ParentCategory { get; set; }
        public IList<CategoryDto>? ChildCategories { get; set; }
        public bool IsShared { get; set; }

         public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>()
            .ForMember(category => category.Id,
            opt => opt.MapFrom(categoryDto => categoryDto.Id))
            .ForMember(category => category.Name,
            opt => opt.MapFrom(categoryDto => categoryDto.Name))
            .ForMember(category => category.CreatorId,
            opt => opt.MapFrom(categoryDto => categoryDto.CreatorId))
            .ForMember(category => category.ParentCategory,
            opt => opt.MapFrom(categoryDto => categoryDto.ParentCategory))
            .ForMember(category => category.ChildCategories,
            opt => opt.MapFrom(categoryDto => categoryDto.ChildCategories));

            profile.CreateMap<CategoryDto, Category>()
            .ForMember(category => category.Id,
            opt => opt.MapFrom(categoryDto => categoryDto.Id))
            .ForMember(category => category.Name,
            opt => opt.MapFrom(categoryDto => categoryDto.Name))
            .ForMember(category => category.CreatorId,
            opt => opt.MapFrom(categoryDto => categoryDto.CreatorId))
            .ForMember(category => category.ParentCategory,
            opt => opt.MapFrom(categoryDto => categoryDto.ParentCategory))
            .ForMember(category => category.ChildCategories,
            opt => opt.MapFrom(categoryDto => categoryDto.ChildCategories));
        }
    }
}