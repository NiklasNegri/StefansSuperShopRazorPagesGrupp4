using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Products
{
    public class ListModel : PageModel
    {
        private readonly IProductService _productService;

        public ListModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public List<Product> ProductList { get; set; } = new List<Product>();

        public async Task<IActionResult> OnGet()
        {
            ProductList = await _productService.GetAll();
            return Page();
        }
    }
}
