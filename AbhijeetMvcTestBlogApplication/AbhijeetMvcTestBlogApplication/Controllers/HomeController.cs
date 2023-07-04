using AbhijeetMvcTestBlogApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AbhijeetMvcTestBlogApplication.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //Home/Login
        public ActionResult Login()
        {
            return View();
        }

        //Home/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var context = new BlogDbContext())
            {
                if(user.UserName == null)
                {
                    ModelState.AddModelError("", "Please enter UserName");
                    return View();
                }
                User user1 = context.Users.FirstOrDefault(uName => uName.UserName == user.UserName);
                if(user1 != null)
                {
                    if(user1.Password == user.Password)
                    {
                        if (user1.Role == "admin")
                        {
                            Session["username"] = user.UserName;
                            FormsAuthentication.SetAuthCookie(user.UserName, false);
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            Session["ID"] = user1.UserId;
                            Session["username"] = user.UserName;
                            FormsAuthentication.SetAuthCookie(user.UserName, false);
                            return RedirectToAction("Index", "Blogs");
                        }
                    }
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "User Not exist");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //Home/Signup
        public ActionResult Signup()
        {
            return View();
        }

        //Home/Signup
        [HttpPost]
        public ActionResult Signup(User user)
        {
            using (var context = new BlogDbContext())
            {
                User user1 = context.Users.FirstOrDefault(uName => uName.UserName == user.UserName);
                if (user1 == null)
                {
                    if(user.UserName != null && user.Password != null)
                    {
                        user.Role = "user";
                        context.Users.Add(user);
                        context.SaveChanges();
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", " UserName already exist");
                }
                return View(user);
            }
        }
    }
}