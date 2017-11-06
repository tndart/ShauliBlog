using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.DAL;
using ShauliBlog.Models;

namespace ShauliBlog.Controllers {
    public class PostsController : Controller {
        private BlogContext db = new BlogContext();

        public IEnumerable<Post> JoinPostWithFans()
        {
            var q = (from post in db.Posts
                     join Fan in db.Fans on post.FanID equals Fan.ID
                     where post.FanID == Fan.ID
                     select post).AsEnumerable();

          
            return q;
        }

        // GET: Posts
        public ActionResult Index() {
            return View(JoinPostWithFans().ToList());
        }



        // Post: Search posts
        public ActionResult SearchPost(string Title, string FanName, string Content, DateTime? PublishDate)
        {
            // Query the all posts
            IQueryable<Post> filteredPosts = db.Posts.AsQueryable();

            // Query by parameters
            if (Title != "")
                filteredPosts = filteredPosts.Where(m => m.Title.Contains(Title));
            if (PublishDate != null)
                filteredPosts = filteredPosts.Where(m => m.PublishedDate.Equals(PublishDate));
            if (FanName != "")
                filteredPosts = filteredPosts.Where(m => m.Fan.FirstName.Contains(FanName) || m.Fan.LastName.Contains(FanName));
            if (Content != "")
                filteredPosts = filteredPosts.Where(m => m.Content.Contains(Content));

            return View("Index", filteredPosts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return Redirect("/Blog/Index/" + id);

        }

        // GET: Posts/StatsByDate/
        public ActionResult Stats(int type) {
            var results = db.Posts.ToList().OrderBy(post => post.PublishedDate);

            if (type == 1) {
                var res = results.GroupBy(post2 => post2.PublishedDate.ToShortDateString(), (key, g) =>
                {
                    var arr = new String[2];
                    arr[0] = g.Count().ToString();
                    arr[1] = key;

                    return arr;
                }).ToArray();

                ViewBag.Title = "Posts per date";

                return View("Stats", res);

            }
            else {
                var res = results.GroupBy(post2 => post2.Fan.Username, (key, g) => {
                    var arr = new String[2];
                    arr[0] = g.Count().ToString();
                    arr[1] = key;

                    return arr;
                }).ToArray();

                ViewBag.Title = "Posts per User";

                return View("Stats", res);

            }
        }

        // GET: Posts/ManageComments
        [Authorize]
        public ActionResult ManageComments(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return Redirect("/Comments/Manage/" + id);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create() {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,FanID,AuthorSiteUrl,PublishedDate,Content,ImageUrl,VideoUrl")] Post post) {
            if (ModelState.IsValid) {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null) {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,FanID,AuthorSiteUrl,PublishedDate,Content,ImageUrl,VideoUrl")] Post post) {
            if (ModelState.IsValid) {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null) {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
