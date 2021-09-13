using AspTask.DAL;
using AspTask.ViewModels;
using Microsoft.AspNetCore.Http;
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
                
            };
           
            return View(homeViewModel);
        }
        public IActionResult AddBasket (int? id)
        {

            return RedirectToAction(nameof(Index));

        }
    }
}
