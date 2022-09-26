using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youtube_Website.Models;

namespace Youtube_Website.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        [AllowAnonymous]
        // GET: User
        public ActionResult SignIn()
        {
            if(Session["IsUserLoggeddIn"] != null)
            {
                if(Request.IsAuthenticated && Request.UrlReferrer!= null && Request.Url != null 
                    && Request.UrlReferrer.Authority.Equals(Request.Url.Authority))
                {
                    return RedirectToAction("Forbidden", "Error");
                }
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        public ActionResult AuthenticateUser(String email, string password)
        {
            UserManager um = new UserManager();
            if (um.SignInCheck(email,password) == true)
            {
                Session["IsUserLoggedIn"] = true;
                Session["email"] = email;
                Session["password"] = password;

                var getuserdata = um.showUserdata(email);

                ViewBag.userdata = getuserdata;
                Session["name"] = getuserdata.name;

                return RedirectToAction("Index", "Home");
            }
            else
                TempData["error"] = "Your email or password is incorrect please try again";
                return RedirectToAction("SignIn", "User");
        }
        public ActionResult SignUp()
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                UserManager um1 = new UserManager();
                um1.AddUserInDb(user);
                Session["name"] = user.name;
                TempData["Massage"] = "Your Account has Created Successfully";
                return RedirectToAction("SignIn","User");
            }
            TempData["Error"] = "Some Error accured while inserting data";
            return RedirectToAction("SignUp", "User");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            
            return RedirectToAction("SignIn", "User");
        }
        public ActionResult updateUserInfo()
        {
            if(Session["IsUserLoggedIn"] != null)
            {

               /* string email = Session["email"].ToString();
                var vm = new UserManager();
                var getuserdata = vm.showUserdata(email);

                ViewBag.userdata = getuserdata;
                Session["name"] = getuserdata.name;*/

                return View();
            }
            return RedirectToAction("SignIn", "User");
        }
        public ActionResult checkupdateDB(string name, string email, string password, string sessionemail)
        {
            // update user data
            // update user data from db
            // pass to video updated data

                if (ModelState.IsValid)
                {
                    UserManager um = new UserManager();
                    sessionemail = Session["email"].ToString();

                    um.updateUserInDB(name, email, password, sessionemail);

                return View("updateUserInfo");
                }
                TempData["Error"] = "Error occured while updating data in DB";
                return RedirectToAction("SignIn", "User");
        }
        public ActionResult getUserDataFromDB(string email)
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                email = Session["email"].ToString();
                var vm = new UserManager();
                var getuserdata = vm.showUserdata(email);

                ViewBag.userdata = getuserdata;
                Session["name"] = getuserdata.name;
                return View("updateUserInfo");
            }
            TempData["Error"] = "Error occured while getting data from DB";
            return RedirectToAction("SignIn", "User");
        }

        public ActionResult deleUserData(string email)
        {
            if (Session["IsUserLoggedIn"] != null)
            {
                email = Session["email"].ToString();
                var vm = new UserManager();
                vm.deleteaccount(email);
                
                return RedirectToAction("SignIn", "User");
            }
            TempData["Error"] = "Error occured while deleting data from DB";
            return RedirectToAction("SignIn", "User");
        }
    }
}