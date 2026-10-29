using data_storage_website_ap5pw.Application.ViewModels;

namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface IFileAppService
    {
        IList<Domain.Entities.File> SelectAll();

        IList<FileCardViewModel> SelectAllCards();

        void Create(Domain.Entities.File file);

        void Edit(Domain.Entities.File file);

        bool Delete(int id);
    }
}