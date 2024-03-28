using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository repository;

        public BlogPostsController(IBlogPostRepository repository)
        {
            this.repository = repository;
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
                UrlHandle = request.UrlHandle
            };

            blogPost = await repository.CreateAsync(blogPost);

            //convert domain model to dto

            var response = new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.ShortDescription, blogPost.UrlHandle,
                blogPost.Content, blogPost.FeaturedImageUrl, blogPost.PublishedDate, blogPost.Author,
                blogPost.IsVisible);

            return Ok(response);
        }
    }
}
