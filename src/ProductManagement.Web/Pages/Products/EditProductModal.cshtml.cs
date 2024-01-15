using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Categories;
using ProductManagement.Products;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Web.Pages.Products
{
    public class EditProductModalModel : ProductManagementPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }
        public SelectListItem[] Categories { get; set; }
        private readonly IProductAppService ProductAppService;
        public EditProductModalModel(IProductAppService productAppService)
        {
            ProductAppService = productAppService;
        }
        public async Task OnGetAsync()
        {
            ProductDto ProductDto = await ProductAppService.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, CreateEditProductViewModel>(ProductDto);
            ListResultDto<CategoryLookupDto> CategoryLookup = await ProductAppService.GetCategoriesAsync();
            Categories = CategoryLookup.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await ProductAppService.UpdateAsync(Id, ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));
            return NoContent();
        }
    }
}
