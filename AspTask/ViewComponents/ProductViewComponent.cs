using AspTask.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTask.ViewComponents
{
    public class ProductViewComponent:ViewComponent
    {
        public AppDbContext _context { get;}
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            var model= _context.Products.Include(p => p.Images)
                .Where(p => p.IsDeleted == false).OrderByDescending(p => p.Id).Take(take);
            return View(await Task.FromResult(model));
        }
    }
}
