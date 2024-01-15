using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace ProductManagement.Products
{
    public class ProductAppService_Tests : ProductManagementApplicationTestBase
    {
        private readonly IProductAppService ProductAppService;

        public ProductAppService_Tests()
        {
            ProductAppService = GetRequiredService<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_Product_List()
        {
            PagedResultDto<ProductDto> output = await ProductAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
            );
            //Assert
            output.TotalCount.ShouldBe(3);
            output.Items.ShouldContain(
                x => x.Name.Contains("Acme Monochrome Laser Printer")
            );
        }
    }
}
