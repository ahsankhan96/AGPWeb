using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AGPWeb.Helpers.Custom;
using AGPWeb.Helpers.General;
using AGPWeb.Models.DB;
namespace AGPWeb.Controllers
{
    public class BaseController : Controller
    {
        protected DbContextOptions<DBContext> dbOption;
        public IWebHostEnvironment hostingEnvironment;

        public BaseController(DbContextOptions<DBContext> dbOptions, IWebHostEnvironment hostingEnvironment)
        {
            this.dbOption = dbOptions;
            this.hostingEnvironment = hostingEnvironment;
        }
        protected tbllogin login;
        List<string> bypassactions = new List<string> { "invoice" };
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            DBContext db = new DBContext(dbOption);
            SessionHelper sessionHelper = new SessionHelper(db, this.Request, this.Response);
            login = sessionHelper.get();

            var action = context.RouteData.Values["action"].ToString().ToLower();
            var controller = context.RouteData.Values["controller"].ToString().ToLower();
            if (bypassactions.Contains(action))
            {
                return;
            }
            if (login == null)
            {
                context.Result = new RedirectResult("/home/logout");
            }
            else
            {
                ViewBag.login = login;
                ViewBag.userId = login.id;
                ViewBag.id = login.id;
                ViewBag.name = login.name;
                ViewBag.username = login.userName;
                ViewBag.basePath = this.hostingEnvironment.WebRootPath;
                ViewBag.roleId = login.roleId;
                ViewBag.photo = login.photo;
                ViewBag.role = login.roleName;


            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
