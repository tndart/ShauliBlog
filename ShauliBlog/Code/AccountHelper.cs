using ShauliBlog.DAL;
using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShauliBlog.Code
{
    public class AccountHelper
    {
        public static bool Login(string userName, string password)
        {
            BlogContext context = new BlogContext();
            Fan user = context.Fans.FirstOrDefault<Fan>(x => x.Username == userName && x.Password == password);
            if (user != null)
            {
                HttpContext.Current.Session["user"] = user;
            }

            return user != null;
        }

        public static void LogOff()
        {
            HttpContext.Current.Session["user"] = null;
        }

        public static bool Register(Models.Fan fan)
        {
            BlogContext context = new BlogContext();

            // check if the user name isn't taken
            if (context.Fans.Any(x => x.Username == fan.Username))
            {
                return false;
            }

            context.Fans.Add(fan);
            context.SaveChanges();
            
            return true;
        }

        public static bool IsAdmin()
        {
            var user = HttpContext.Current.Session["user"];
            return (user != null && ((Fan)user).IsAdmin);
        }

        public static string Username()
        {
            var user = HttpContext.Current.Session["user"];
            return user != null ? ((Fan)user).Username : "";
        }

        // only if the user is the owner of the post/comment or is admin he is authorized
        public static bool IsAuthorized(string owner)
        {
            var user = HttpContext.Current.Session["user"];
            if (user == null) return false;
            return ((((Fan)user).IsAdmin) || ((Fan)user).Username.Equals(owner, StringComparison.CurrentCultureIgnoreCase));
        }



    }
}
