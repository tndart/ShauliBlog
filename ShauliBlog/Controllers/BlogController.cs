using ShauliBlog.Controllers;
using ShauliBlog.DAL;
using ShauliBlog.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers {
    public class BlogController : Controller {
        private BlogContext db = new BlogContext();

        public ActionResult Index(int? id) {
            ViewBag.Selected = "Blog";

            if (id == null)
                id = 1;

            Post post = db.Posts.Find(id);
            if (post == null) {
                return HttpNotFound();
            }

            return View(post);
        }

        public ActionResult PostsByTag(string tags) {

            ViewBag.Selected = "Blog";

            if (tags == null || tags.Length == 0) {
                return HttpNotFound();
            }

            // Separate between the 'tags' param.
            // "1-2" => array[] {1, 2}
            string[] strTagsID = tags.Split('-').ToArray();
            int[] nTagsID = new int[strTagsID.Length - 1];

            for (int i = 0; i < strTagsID.Length - 1; i++) {
                try {
                    nTagsID[i] = int.Parse(strTagsID[i]);
                }
                catch (Exception) {
                    nTagsID[i] = -1;
                }
            }

            // Select all tags
            var tagList = from t in db.Tags
                          select t;

            // filter the tags based on the tags from param list
            List<Tag> tagList1 = new List<Tag>();
            foreach (int id in nTagsID) {
                tagList1.AddRange(tagList.Where(t => t.ID == id).ToList());
            }

            // Select all posts
            var posts = from post in db.Posts
                        select post;

            // filter the posts
            List<Post> posts1 = new List<Post>();
            foreach (var tag in tagList1) {
                posts1.AddRange(posts.Where(p => p.Tags.Any(t => t.ID == tag.ID)));
            }

            posts1 = posts1.Distinct().ToList();

            if (posts1 == null || posts1.Count() == 0) {
                return HttpNotFound();
            }

            return View(posts1);
        }
    }
}