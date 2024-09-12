using AGPWeb.Helpers.Custom;
using AGPWeb.Helpers.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using AGPWeb.Models.DB;
namespace AGPWeb.Controllers
{
	public class PublicBaseController : Controller
	{
		protected DbContextOptions<DBContext> dbOption;
		IWebHostEnvironment hostingEnvironment;
		public PublicBaseController(DbContextOptions<DBContext> dbOptions, IWebHostEnvironment hostingEnvironment)
		{
			this.dbOption = dbOptions;
			this.hostingEnvironment = hostingEnvironment;
		}
		protected tbllogin login;
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			using (DBContext db = new DBContext(dbOption))
			{
				base.OnActionExecuting(context);
				SessionHelper sessionHelper = new SessionHelper(db, this.Request, this.Response);
				login = sessionHelper.get();
				ViewBag.login = login;
			}
		}
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}
	}
}
