namespace data_storage_website_ap5pw.Domain.Entities.Interfaces
{
public interface IEntity<TKey> where TKey : notnull
    {
        TKey Id { get; set; }
    }
}