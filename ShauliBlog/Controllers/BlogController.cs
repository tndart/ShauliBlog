using ShauliBlog.DAL;
using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index(int? id)
        {
            ViewBag.Selected = "Blog";

            if (id == null)
                id = 1;

            Post post = db.Posts.Find(id);
            if (post == null) {
                return HttpNotFound();
            }

            return View(post);
        }
    }
}