using data_storage_website_ap5pw.Application.ViewModels;

namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface IHomeService
    {
        IndexViewModel GetIndexViewModel();
    }
}