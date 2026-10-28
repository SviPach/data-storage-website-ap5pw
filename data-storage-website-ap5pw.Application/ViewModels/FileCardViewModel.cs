namespace data_storage_website_ap5pw.Application.ViewModels
{
    public class FileCardViewModel
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;

        public string AuthorFirstName { get; set; } = string.Empty;

        public string AuthorLastName { get; set; } = string.Empty;
    }
}