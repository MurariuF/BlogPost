﻿using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await repository.CreateAsync(category);

            var response = new CategoryDto(category.Id, category.Name, category.UrlHandle);

            return Ok(response);
        }

        // get: https://localhost:7014/api/Categories?query=html&sortBy=name&sortDirection=asc
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var categories = await repository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);
            
            //map domain model to dto
            var response = new List<CategoryDto>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto(category.Id, category.Name, category.UrlHandle));
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var category = await repository.GetCategoryByIdAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto(category.Id, category.Name, category.UrlHandle);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryDto updateCategory)
        {
            //convert dto to domain model
            var category = new Category
            {
                Id = id,
                Name = updateCategory.Name,
                UrlHandle = updateCategory.UrlHandle
            };

            category = await repository.UpdateAsync(category);

            if (category is null)
            {
                return NotFound();
            }

            //convert domain model to dto
            var response = new CategoryDto(category.Id, category.Name, category.UrlHandle);

            return Ok(response); 
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {
            var category = await repository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto(category.Id, category.Name, category.UrlHandle);

            return Ok(response);
        }

        //get: https://localhost:7014/api/categories/count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCategoriesTotal()
        {
            var count = await repository.GetTotalCount();

            return Ok(count);
        }
    }
}
