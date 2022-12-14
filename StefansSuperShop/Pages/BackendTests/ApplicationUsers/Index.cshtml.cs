using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;

namespace StefansSuperShop.Pages.BackendTests.ApplicationUsers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> ApplicationUsers { get; set; }

        public async Task OnGetAsync()
        {
            ApplicationUsers = await _context.ApplicationUsers.ToListAsync();
        }
    }
}
