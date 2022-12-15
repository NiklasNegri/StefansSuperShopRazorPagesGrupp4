using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Newsletters
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Newsletter> Newsletter { get;set; }

        public async Task OnGetAsync()
        {
            Newsletter = await _context.Newsletters.ToListAsync();
        }
    }
}
