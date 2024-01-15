using ProductManagement.Models.Categories;
using ProductManagement.Models.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Data
{
    public class ProductManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<ProductCategory, Guid> CategoryRepository;
        private readonly IRepository<Product, Guid> ProductRepository;

        public ProductManagementDataSeedContributor(IRepository<ProductCategory, Guid> categoryRepository, IRepository<Product, Guid> productRepository)
        {
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await CategoryRepository.CountAsync() > 0)
            {
                return;
            }
            var Monitors = new ProductCategory { Name = "Monitors" };
            var Printers = new ProductCategory { Name = "Printers" };
            await CategoryRepository.InsertManyAsync(new[] { Monitors, Printers });
            var Monitor1 = new Product
            {
                Category = Monitors,
                Name = "XP VH240a 23.8-Inch Full HD 1080p IPS LED Monitor",
                Price = 163,
                ReleaseDate = new DateTime(2019, 05, 24),
                StockState = ProductStockState.InStock,
            };
            var Monitor2 = new Product
            {
                Category = Monitors,
                Name = "Clips 328E1CA 32-Inch Curved Monitor, 4K UHD",
                Price = 349,
                ReleaseDate = new DateTime(2022, 02, 01),
                StockState = ProductStockState.PreOrder,
            };
            var Printer1 = new Product
            {
                Category = Monitors,
                Name = "Acme Monochrome Laser Printer, Compact All-In One",
                Price = 199,
                ReleaseDate = new DateTime(2020, 11, 16),
                StockState = ProductStockState.NotAvailable,
            };
            await ProductRepository.InsertManyAsync(new[] { Monitor1, Monitor2, Printer1 });
        }
    }
}
