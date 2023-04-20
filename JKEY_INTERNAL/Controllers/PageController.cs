
using JKEY_INTERNAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace JKEY_INTERNAL.Controllers
{
    public class PageController : Controller
    {
        #region Feild


        private readonly ILogger<PageController> _logger;
        private readonly JkeyInternalContext _context;


        public PageController(ILogger<PageController> logger, JkeyInternalContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }


        #endregion


        public IActionResult Index()
        {
            List<Page> Items = _context.Pages.ToList();
            //int Count = Items.Count();
            //return Ok(new { Items, Count });
            return View(Items);
        }

    }
}
