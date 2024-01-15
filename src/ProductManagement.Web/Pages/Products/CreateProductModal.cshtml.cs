using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Categories;
using ProductManagement.Models.Categories;
using ProductManagement.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Web.Pages.Products
{
    public class CreateProductModalModel : ProductManagementPageModel
    {
        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }
        public SelectListItem[] Categories { get; set; }
        private readonly IProductAppService ProductAppService;
        
        public CreateProductModalModel(IProductAppService productAppService)
        {
            ProductAppService = productAppService;
        }

        public async Task OnGetAsync()
        {
            Product = new CreateEditProductViewModel
            {
                ReleaseDate = Clock.Now,
                StockState = ProductStockState.PreOrder
            };
            ListResultDto<CategoryLookupDto> CategoryLookup = await ProductAppService.GetCategoriesAsync();
            Categories = CategoryLookup.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await ProductAppService.CreateAsync(ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));
            return NoContent();
        }
    }
}
