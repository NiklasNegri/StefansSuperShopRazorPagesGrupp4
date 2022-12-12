using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data;

namespace StefansSuperShop.Pages.Admin
{
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ProductRow> Products { get; set; }

        public class ProductRow
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stocklevel { get; set; }

        }

        public ProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Products = _context.Products.Select(e=>new ProductRow
            {
                Name = e.ProductName,
                Price = e.UnitPrice.Value,
                Stocklevel = e.UnitsInStock.Value,
                Id = e.ProductId

            }).ToList();
        }
    }
}
