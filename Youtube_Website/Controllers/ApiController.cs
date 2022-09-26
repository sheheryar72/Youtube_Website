using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youtube_Website.Models;

namespace Youtube_Website.Controllers
{
    [ha]
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult GetUsersList()
        {
            UserManager um = new UserManager();
            List<User> users = um.GetUserList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserByID()
        {
            return View();
        }
        public ActionResult GetVideos()
        {
            var um = new VideoManager();
            List<Video> alluservideos = um.GetAllVideos();
            return Json(alluservideos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVideosByID()
        {   
            return View();
        }
    }
}