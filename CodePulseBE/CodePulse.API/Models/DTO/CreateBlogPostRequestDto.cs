namespace CodePulse.API.Models.DTO
{
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UrlHandle { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public CreateBlogPostRequestDto(string title, string shortDescription, string urlHandle, string content, string featuredImageUrl, DateTime publishedDate, string author, bool isVisible)
        {
            Title = title;
            ShortDescription = shortDescription;
            UrlHandle = urlHandle;
            Content = content;
            FeaturedImageUrl = featuredImageUrl;
            PublishedDate = publishedDate;
            Author = author;
            IsVisible = isVisible;
        }
    }
}
