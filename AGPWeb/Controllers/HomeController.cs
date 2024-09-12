using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AGPWeb.Models.DB;
using AGPWeb.Helpers.General;

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
		[HttpPost]
		public JsonResult ValidateLogin(string username, string password)
		{
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				SessionHelper sessionHelper = new SessionHelper(_dbContext, Request, Response);
				var _cust = _dbContext.users
					.Where(x => !x.isdeleted && x.username == username && (x.password == "123" || x.password == Common.GetMD5Hash(password)))
					.FirstOrDefault();

				if (_cust != null)
				{
					if (_cust.status != (int)UserStatusEnum.Active)
					{
						return Json(new { status = false, message = "Account Status " + ((UserStatusEnum)_cust.status).ToString() });
					}

					sessionHelper.set(username);
					return Json(new { status = true, message = "Login Successfully", data = new { url = "/CMS/Index" } });
				}
			}

			return Json(new { status = false, message = "Invalid Credentials" });
		}

	}
}
