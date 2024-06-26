﻿namespace CodePulse.API.Models.DTO
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UrlHandle { get; set; }
        public string Content { get; set; }
        public string FeatureImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public List<CategoryDto> Categories { get; set; }

        public BlogPostDto(Guid id, string title, string shortDescription, string urlHandle, string content, string featureImageUrl, DateTime publishedDate, string author, bool isVisible, List<CategoryDto> categories)
        {
            Id = id;
            Title = title;
            ShortDescription = shortDescription;
            UrlHandle = urlHandle;
            Content = content;
            FeatureImageUrl = featureImageUrl;
            PublishedDate = publishedDate;
            Author = author;
            IsVisible = isVisible;
            Categories = categories;
        }
    }
}
