namespace CodePulse.API.Models.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public CategoryDto(Guid id, string name, string urlHandle)
        {
            Id = id;
            Name = name;
            UrlHandle = urlHandle;
        }
    }
}
