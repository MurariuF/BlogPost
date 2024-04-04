namespace CodePulse.API.Models.DTO
{
    public class BlogImageDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }

        public BlogImageDto(Guid id, string fileName, string fileExtension, string title, string url)
        {
            this.Id = id;
            this.FileName = fileName;
            this.FileExtension = fileExtension;
            this.Title = title;
            this.Url = url;
            this.DateCreated = DateTime.Now;
        }
    }
}
