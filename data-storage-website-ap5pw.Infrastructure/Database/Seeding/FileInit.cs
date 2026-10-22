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
                FileName = "test1",
                Size = 1,
                StoragePath = "/test/1"
            });
            files.Add(new Domain.Entities.File
            {
                Id = 2,
                FileName = "test2",
                Size = 2,
                StoragePath = "/test/2"
            });
            files.Add(new Domain.Entities.File
            {
                Id = 3,
                FileName = "test3",
                Size = 3,
                StoragePath = "/test/3"
            });

            return files;
        }
    }
}