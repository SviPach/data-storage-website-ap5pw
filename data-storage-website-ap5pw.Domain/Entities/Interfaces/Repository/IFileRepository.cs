using data_storage_website_ap5pw.Domain.Records;

namespace data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository
{
    public interface IFileRepository : IRepository<Domain.Entities.File, int>
    {
        IList<FileCardRecord> SelectAllCards();
    }
}