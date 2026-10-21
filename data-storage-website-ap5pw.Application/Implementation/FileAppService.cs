using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;

namespace data_storage_website_ap5pw.Application.Implementation
{
    public class FileAppService : IFileAppService
    {
        IFileRepository _fileRepository;

        public FileAppService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public IList<Domain.Entities.File> SelectAll()
        {
            return _fileRepository.GetAll().ToList();
        }

        public void Create(Domain.Entities.File file)
        {
            _fileRepository.Add(file);
        }
    }
}