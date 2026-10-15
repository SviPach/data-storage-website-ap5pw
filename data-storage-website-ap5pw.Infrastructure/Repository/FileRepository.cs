using System;
using System.Collections.Generic;
using System.Text;
using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;
using data_storage_website_ap5pw.Infrastructure.Database;
using data_storage_website_ap5pw.Domain.Infrastructure.Repository;

namespace data_storage_website_ap5pw.Infrastructure.Repository
{
    public class FileRepository : Repository<Domain.Entities.File, int>, IFileRepository
    {
        public FileRepository(DataStorageDbContext context) : base(context)
        {
        }
    }
}