
using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;
using data_storage_website_ap5pw.Domain.Records;

namespace data_storage_website_ap5pw.Application.Implementation
{
    public class FileAppService : IFileAppService
    {
        private readonly IFileRepository _fileRepository;

        public FileAppService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public IList<Domain.Entities.File> SelectAll()
        {
            return _fileRepository.GetAll().ToList();
        }

        public IList<FileCardViewModel> SelectAllCards()
        {
            return _fileRepository.SelectAllCards()
                .Select(record => new FileCardViewModel
                {
                    Id = record.Id,
                    FileName = record.FileName,
                    Topic = record.Topic,
                    AuthorFirstName = record.AuthorFirstName,
                    AuthorLastName = record.AuthorLastName
                })
                .ToList();
        }

        public void Create(Domain.Entities.File file)
        {
            _fileRepository.Add(file);
        }

        public void Edit(Domain.Entities.File file)
        {
            _fileRepository.Edit(file);
        }

        public bool Delete(int id)
        {
            Domain.Entities.File? file = _fileRepository.GetById(id);

            if (file == null)
            {
                return false;
            }

            _fileRepository.Delete(file);
            return true;
        }
    }
}
