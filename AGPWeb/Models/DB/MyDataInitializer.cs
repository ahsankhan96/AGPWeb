using Microsoft.EntityFrameworkCore;

namespace AGPWeb.Models.DB
{
    public class MyDataInitializer
    {
        public static void SeedData(DbContextOptions<DBContext> dbOptions, string webroot)
        {
            using (DBContext db = new DBContext(dbOptions))
            {

                if (!db.roles.Any())
                {
                    db.roles.Add(new Role { name = "Admin" });
                    db.roles.Add(new Role { name = "HOD" });
                    db.roles.Add(new Role { name = "Security" });
                    db.roles.Add(new Role { name = "Initiator" });


                }
                if (!db.users.Any())
                {
                    db.users.Add(new User
                    {
                        username = "Admin",
                        email = "aa@example.com",
                        password = "123",
                        roleId = 9,
                        status = 1
                    });

                }
                db.SaveChanges();

            }
        }

    }
}
