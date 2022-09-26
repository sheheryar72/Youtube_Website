using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youtube_Website.Models;

namespace Youtube_Website.Controllers
{
    [HandleError]
    public class VideoController : Controller
    {
        // video player
        // GET: Video
        public ActionResult Play(int id)
        {
                // get video record from db
                // pass video details to view
                VideoManager vm = new VideoManager();
                var model = vm.GetVideoByID(id);

                var myFirstCookie = new HttpCookie("myFirstCoocke", "myDummyCoockie");

                Response.Cookies.Set(myFirstCookie);

                return View(model);
                
        }
        // upload video
        public ActionResult Upload()
        {
            // show user video fields
            if(Session["IsUserLoggedIn"] != null)
           {
                return View("Upload");
           }
           return RedirectToAction("SignIn","User");
        }
        // upload video form ation in postvideo
        [HttpPost]
        public ActionResult PostVideo(string title, string description, string source, string uploadedby, string uploadedat)
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                if (ModelState.IsValid)
                {
                    //Video v = new Video();
                    //title = v.title;
                    //description =  v.description;
                    uploadedby = Session["email"].ToString();
                    uploadedat = DateTime.Now.ToString();
                    //source = v.source;
                    VideoManager vm1 = new VideoManager();
                    vm1.UploadVideoinDB(title, description, source, uploadedby, uploadedat);
                    return View("Upload");
                    TempData["Message"] = "Video Uploaded Successfully";
                }
                TempData["VideoUploadError"] = "Some error occured while inserting video in db";
            }
            return RedirectToAction("SignIn", "User");
        }
        // show myvideos
        public ActionResult MyVideos(string uploaderemail)
        {
            // get current user video
                uploaderemail = Session["email"].ToString();

                VideoManager vm = new VideoManager();
                var myvideo1 = vm.GetVideoByUploader(uploaderemail);

                ViewBag.myvideo = myvideo1;
                return View();
        }
        [HttpGet]
        public ActionResult UserVideos(string uploaderemail) //kia horaha hai ye?
        {
            //string uploaderemail = form["uploaderemail"];
            //emailvideo ev = new emailvideo();
            //uploaderemail = ev.uploaderemail;

            VideoManager vm = new VideoManager();

            var myvideo1 = vm.GetVideoByUploader(uploaderemail);

            ViewBag.myvideo = myvideo1;

            return View();
        }
        [HttpGet]
        public ActionResult EditVideo(int id)
        {
            if(Session["IsUserLoggedIn"] != null)
            {
             string uploaderemail = Session["email"].ToString();
            ViewBag.VideoIDGet = id;

            VideoManager vm = new VideoManager();
            var myvideo1 = vm.GetVideoByUploader(uploaderemail);

            ViewBag.myvideo = myvideo1;

            return View();
            }
            return RedirectToAction("SignIn", "User");
        }
        public ActionResult CheckUpdateVideo(int id, string title, string description, string source, string uploadedat)
        {
            if (ModelState.IsValid)
            {
                uploadedat = DateTime.Now.ToString();
                VideoManager vd = new VideoManager();
                vd.UpdateVideoInDB(id, title, description, source, uploadedat);

                return RedirectToAction("Index", "Home");
                /*return RedirectToAction("EditVideo", "Video");*/
            }
            return RedirectToAction("MyVideos", "Video");
            TempData["Error"] = "Error occured while updating video";
        }
        public ActionResult CheckDeleteVideo(int id)
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                var vm = new VideoManager();
                vm.DeleteVideoInDB(id);

                return RedirectToAction("MyVideos", "Video");
            }
            return RedirectToAction("SignIn", "User");
        }
        public ActionResult VideosList()
        {
            return View();
        }
    }  
}