using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AGPWeb.Models.DB;

namespace AGPWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment hostingEnvironment, DbContextOptions<DBContext> dbOptions )
        {
            _hostingEnvironment = hostingEnvironment;
            _dbContext = new DBContext(dbOptions);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
