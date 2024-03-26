namespace CodePulse.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public bool IsVisible { get; set; }
        public string UrlHandle { get; set; }
    }
}
