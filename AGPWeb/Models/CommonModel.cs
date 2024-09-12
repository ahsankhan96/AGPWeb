using Microsoft.EntityFrameworkCore;
using AGPWeb.Helpers.Custom;
using AGPWeb.Models.DB;
namespace AGPWeb.Models
{
    public class CommonModel
    {
        public static tbllogin GetUserInfo(DBContext db, string username)
        {
            tbllogin login = null;

            var a = db.users.Where(x => !x.isdeleted && (x.username == username)).Include(x => x.role).FirstOrDefault();
            if (a != null)
            {
                return new tbllogin
                {
                    id = a.id,
                    name = a.name,
                    photo = a.photo,
                    email = a.email,
                    roleId = a.roleId,
                    roleName = a.role.name,
                    userName = a.username,
                };
            }
            return login;
        }
        public static bool IsUserNameAvaliable(DBContext db, string username)
        {
            if (db.users.Any(x => !x.isdeleted && x.username == username)) { return false; }
            return true;
        }

        private static DbContextOptions<DBContext> _dbOption = null;
        public static DbContextOptions<DBContext> GetDBOption()
        {
            if (_dbOption == null)
            {
                var AppSetting = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
                string connectionStr = AppSetting.GetConnectionString("DBC");
                optionsBuilder.UseSqlServer(connectionStr);
                _dbOption = optionsBuilder.Options;
                return optionsBuilder.Options;
            }
            else { return _dbOption; }
        }
    }
}
