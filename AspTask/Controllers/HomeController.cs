using AspTask.DAL;
using AspTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTask.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Slides =await _context.Slides.ToListAsync(),
                Introduction =await _context.Introduction.FirstOrDefaultAsync(),
                Categories=await _context.Categories.Where(c=>c.IsDeleted==false).ToListAsync(),
                Products=await _context.Products.Where(p=>p.IsDeleted==false)
                .Include(p=>p.Images).Include(p=>p.Category).OrderByDescending(p=>p.Id).Take(8).ToListAsync()
            };
            return View(homeViewModel);
        }
    }
}
