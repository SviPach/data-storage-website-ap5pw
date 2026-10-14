using data_storage_website_ap5pw_Domain.Entities.Interfaces;

namespace UTB.Eshop.Domain.Entities
{
    public class Entity<TKey> : IEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; set; }
    }
}