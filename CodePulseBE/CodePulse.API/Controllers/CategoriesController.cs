using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
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
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await repository.CreateAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
        // get: https://localhost:7014/api/Categories

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await repository.GetAllAsync();
            
            //map domain model to dto
            var response = new List<CategoryDto>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id=category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
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

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
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
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response); 
        }
    }
}
