using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youtube_Website.Models;

namespace Youtube_Website.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // we can use var instead of VideoManager again
            var vm = new VideoManager();
            var videos = vm.GetAllVideos();
            // In ViewData we can only pass object to view
            // In Viewbad we can pass all object to view dynamic property
            ViewBag.videos = videos;
            //ViewBag.email = Session["email"];


            /*string email = Session["email"].ToString();
            var um = new UserManager();
            var getuserdata =um.showUserdata(email);

            ViewBag.userdata = getuserdata;
            Session["name"] = getuserdata.name;*/

            return View(videos);
        }

        public ActionResult SearchVideoList(string keyword)
        {
            VideoManager vm = new VideoManager();
            var searchVideo = vm.SearchVideoInDB(keyword);

            ViewBag.searchVideo = searchVideo;

            return View(searchVideo);
        }
        public ActionResult UserList()
        {
            return View();
        }
        
    }
}