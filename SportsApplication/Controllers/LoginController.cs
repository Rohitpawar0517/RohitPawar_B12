using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsApplication;
using System.Web.Security;

namespace SportsApplication.Controllers
{
    public class LoginController : Controller
    {
        
        [HttpGet]
        public ActionResult Login()
        {
            /*List<string> list = new List<string>() {"Coach","Captain","Player"};
            ViewBag.list = list;*/
            return View();
        }

        [HttpPost,ActionName("Login")]
        public ActionResult Loginpost(Registration reg,string EmailId,String Password)
        {
            using (var context=new IndiaDBEntities())
            {
                /*var result = (from user in context.Users
                              join role in context.UserRoles on user.Id equals role.UserId
                              where user.Email == username
                              select role.Roles).ToArray();*/
                //var a = context.Registrations.Any(x => x.EmailId == reg.EmailId && x.Password == reg.Password);


                if (context.Registrations.Any(x => x.EmailId == reg.EmailId && x.Password == reg.Password))
                {

                    /* SqlParameter param1 = new SqlParameter("@EmailId", reg.EmailId);
                     SqlParameter param2 = new SqlParameter("@Password", reg.Password);
                     var db = new IndiaDBEntities();
                     var a = db.Database.SqlQuery<Registration>("exec usp_getUserType @EmailId @Password", param1,param2);
 */                    /* var a = (from user in context.Registrations
                                    where EmailId == user.EmailId && Password == user.Password
                                    select user.UserType).ToString();*/
                    /*var a = from item in context.Registrations
                                              where item.EmailId == "rohitpawar3222@gmail.com" && item.Password == "rohit222"
                                              select reg.UserType;*/

                    // var a = "TeamCoach";

                    string a = (from x in context.Registrations
                                where x.EmailId == EmailId && x.Password == Password
                                select x.UserType).SingleOrDefault();

                   // ViewBag.list = a;

                    if (a.Equals("TeamCoach"))
                    {
                        FormsAuthentication.SetAuthCookie(EmailId,false);
                        return RedirectToAction("Index","TeamCoach");
                    }
                    else if(a.Equals("Captain"))
                    {
                        FormsAuthentication.SetAuthCookie(EmailId,false);
                        return RedirectToAction("Index","Captain");
                    }
                    else if(a.Equals("Player"))
                    {
                        string list = (from x in context.Registrations
                                       where x.EmailId == EmailId && x.Password == Password
                                       select x.Status).SingleOrDefault();
                        TempData["mydata"] = list;
                        //ViewBag.list = list;
                        FormsAuthentication.SetAuthCookie(EmailId, false);
                        return RedirectToAction("PlayerMessage","Player");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                     ModelState.AddModelError("", "Invalid Credential");
            }
            return View();
        }
        public ActionResult Coach()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }

    }
}