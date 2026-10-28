
using System.Linq;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;
using data_storage_website_ap5pw.Domain.Infrastructure.Repository;
using data_storage_website_ap5pw.Domain.Records;
using data_storage_website_ap5pw.Infrastructure.Database;

namespace data_storage_website_ap5pw.Infrastructure.Repository
{
    public class FileRepository
        : Repository<Domain.Entities.File, int>, IFileRepository
    {
        private readonly DataStorageDbContext _context;

        public FileRepository(DataStorageDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IList<FileCardRecord> SelectAllCards()
        {
            var cards =
                from file in _context.Files
                join user in _context.Users
                    on file.AuthorId equals user.Id
                select new FileCardRecord
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    Topic = file.Topic,
                    AuthorFirstName = user.FirstName ?? string.Empty,
                    AuthorLastName = user.LastName ?? string.Empty
                };

            return cards.ToList();
        }
    }
}
