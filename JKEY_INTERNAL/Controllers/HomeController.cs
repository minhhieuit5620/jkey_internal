//using JKEY_COMMON.Entities;
using JKEY_DL.DataContext;
using JKEY_INTERNAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JKEY_INTERNAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JkeyInternalContext _context;


        public HomeController(ILogger<HomeController> logger, JkeyInternalContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }

        public IActionResult Index()
        {

            List<Menu> Items =  _context.Menus.ToList();
            //int Count = Items.Count();
            //return Ok(new { Items, Count });
            return View(Items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}