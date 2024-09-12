using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AGPWeb.Models.DB;

namespace AGPWeb.Controllers
{
    public class CMSController : BaseController
    {
        private readonly DBContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CMSController(IWebHostEnvironment hostingEnvironment, DbContextOptions<DBContext> dbOptions) : base(dbOptions, hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _dbContext = new DBContext(dbOptions);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
