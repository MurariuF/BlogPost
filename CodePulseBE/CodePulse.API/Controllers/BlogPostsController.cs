using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            //Convert DTO to Domain
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryGuid);

                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            //convert domain model to dto

            var response = new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.ShortDescription, blogPost.UrlHandle,
                blogPost.Content, blogPost.FeaturedImageUrl, blogPost.PublishedDate, blogPost.Author,
                blogPost.IsVisible, blogPost.Categories.Select(x => new CategoryDto(x.Id, x.Name, x.UrlHandle)).ToList());

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogposts()
        {
            var blogposts = await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogposts)
            {
                response.Add(new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.ShortDescription, blogPost.UrlHandle,
                    blogPost.Content, blogPost.FeaturedImageUrl, blogPost.PublishedDate, blogPost.Author,
                    blogPost.IsVisible, blogPost.Categories.Select(x => new CategoryDto(x.Id, x.Name, x.UrlHandle)).ToList()));
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var response = await blogPostRepository.GetByIdAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            var responseDto = new BlogPostDto(response.Id, response.Title, response.ShortDescription,
                response.UrlHandle,
                response.Content, response.FeaturedImageUrl, response.PublishedDate, response.Author,
                response.IsVisible,
                response.Categories.Select(x => new CategoryDto(x.Id, x.Name, x.UrlHandle)).ToList());

            return Ok(responseDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            //Convert DTO to Domain
            var blogPost = new BlogPost
            {
                Id = id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            // foreach

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryGuid);

                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.ShortDescription, blogPost.UrlHandle,
                blogPost.Content, blogPost.FeaturedImageUrl, blogPost.PublishedDate, blogPost.Author,
                blogPost.IsVisible, blogPost.Categories.Select(x => new CategoryDto(x.Id, x.Name, x.UrlHandle)).ToList());

            return Ok(response);
        }
    }
}
