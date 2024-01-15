using System;
using ProductManagement.Models.Categories;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement.Models.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public ProductCategory Category { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ProductStockState StockState { get; set; }
    }
}
