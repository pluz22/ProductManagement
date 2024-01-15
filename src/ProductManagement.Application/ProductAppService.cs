using ProductManagement.Categories;
using ProductManagement.Models.Categories;
using ProductManagement.Models.Products;
using ProductManagement.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement
{
    public class ProductAppService : ProductManagementAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> ProductRepository;
        private readonly IRepository<ProductCategory, Guid> CategoryRepository;
        public ProductAppService(IRepository<Product, Guid> productRepository, IRepository<ProductCategory, Guid> categoryRepository)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CreateUpdateProductDto input)
        {
            await ProductRepository.InsertAsync(
                ObjectMapper.Map<CreateUpdateProductDto, Product>(input)
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            await ProductRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Product, ProductDto>(
                await ProductRepository.GetAsync(id)
            );
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            List<ProductCategory> Categories = await CategoryRepository.GetListAsync();
            return new ListResultDto<CategoryLookupDto>(ObjectMapper.Map<List<ProductCategory>, List<CategoryLookupDto>>(Categories));
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            IQueryable<Product> Queryable = await ProductRepository.WithDetailsAsync(x => x.Category);
            Queryable = Queryable.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(input.Sorting ?? nameof(Product.Name));
            List<Product> Products = await AsyncExecuter.ToListAsync(Queryable);
            long Count = await ProductRepository.GetCountAsync();
            return new PagedResultDto<ProductDto>(
                Count,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(Products)
            );
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            Product product = await ProductRepository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }
    }
}
