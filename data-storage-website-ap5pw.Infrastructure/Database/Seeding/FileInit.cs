using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Infrastructure.Database.Seeding
{
    internal class FileInit
    {
        public IList<Domain.Entities.File> GetFiles3()
        {
            IList<Domain.Entities.File> files = new List<Domain.Entities.File>();

            files.Add(new Domain.Entities.File
            {
                Id = 1,
                FileName = "file1",
                Topic = "Topic1",
                Text = "Some text about topic1.",
                AuthorId = 1
            });
            files.Add(new Domain.Entities.File
            {
                Id = 2,
                FileName = "file2",
                Topic = "Topic2",
                Text = "Some text about topic2.",
                AuthorId = 1
            });
            files.Add(new Domain.Entities.File
            {
                Id = 3,
                FileName = "file3",
                Topic = "Topic3",
                Text = "Some text about topic3.",
                AuthorId = 1
            });

            return files;
        }
    }
}