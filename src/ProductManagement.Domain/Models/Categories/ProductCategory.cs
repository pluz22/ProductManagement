using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement.Models.Categories
{
    public class ProductCategory : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
