using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AGPWeb.Models.DB
{
    public class PComm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public bool isdeleted { get; set; }
    }
    public class Role:PComm
    {
        public string name { get; set; }
        public ICollection<User> users { get; set; }
    }

    public class User:PComm
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public DateTime? lastlogin { get; set; }
        public string resetPasswordToken { get; set; }
        public DateTime? resetPasswordTokenExpiry { get; set; }


        [ForeignKey("role")]
        public int roleId { get; set; }
        public virtual Role role { get; set; }
    }
}
