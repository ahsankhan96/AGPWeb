using Microsoft.AspNetCore.Mvc.ApplicationModels;
using AGPWeb.Helpers.Custom;
using AGPWeb.Models.DB;
using AGPWeb.Models;

namespace AGPWeb.Helpers.General
{
    public class SessionHelper
    {
        HttpRequest _request;
        HttpResponse _response;
        DBContext _db;
        public SessionHelper(DBContext db, HttpRequest request, HttpResponse response)
        {
            this._request = request;
            this._response = response;
            this._db = db;
        }


        public void set(string username)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(30);
            var u = _db.users.Where(x => x.username == username).FirstOrDefault();
            u.lastlogin = DateTime.Now;
            _db.SaveChanges();
            _response.Cookies.Append(Constants.LoginKey, Common.Encrypt(username, Constants.EncKey), option);
        }
        public tbllogin get()
        {
            var value = _request.Cookies[Constants.LoginKey];
            if (!string.IsNullOrEmpty(value))
            {

                var username = Common.Decrypt(value, Constants.EncKey);
                var Data = CommonModel.GetUserInfo(_db, username);
                return Data;
            }
            return null;
        }

        public void delete()
        {
            try
            {
                _response.Cookies.Delete(Constants.LoginKey);
            }
            catch { }
        }


    }
}
