using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Queries.Categories.GetCategories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CategoriesVm>> GetAll()
        {
            var query = new GetCategoriesQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator!.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCategoryCommand>> Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            var command = _mapper.Map<CreateCategoryCommand>(createCategoryDto);
            command.CreatorId = UserId;
            var categoryId = await Mediator!.Send(command);

            return Ok(categoryId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand
            {
                Id = id,
                CreatorId = UserId
            };

            await Mediator!.Send(command);
            return NoContent();
        }
    }
}