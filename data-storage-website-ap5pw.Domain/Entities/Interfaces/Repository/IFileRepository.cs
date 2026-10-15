using System;
using System.Collections.Generic;
using System.Text;
using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository
{
    public interface IFileRepository : IRepository<Domain.Entities.File, int>
    {
    }
}