using Microsoft.EntityFrameworkCore;

namespace AGPWeb.Models.DB
{
    public class MyDataInitializer
    {
        public static void SeedData(DbContextOptions<DBContext> dbOptions, string webroot)
        {
            using (DBContext db = new DBContext(dbOptions))
            {

                //if (!db.roles.Any())
                //{
                //    db.roles.Add(new Role { name = "Admin" });

                //}
                //if (!db.users.Any())
                //{
                //    db.users.Add(new User
                //    {
                //        id = 1,
                //        username = "Admin",
                //        email = "aa@example.com",
                //        password = "123",
                //        roleId = 1
                //    });

                //}
                //db.SaveChanges();

            }
        }

    }
}
