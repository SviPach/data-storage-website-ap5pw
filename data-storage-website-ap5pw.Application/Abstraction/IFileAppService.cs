using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface IFileAppService
    {
        IList<Domain.Entities.File> SelectAll();
        void Create(Domain.Entities.File file);
        bool Delete(int id);
    }
}