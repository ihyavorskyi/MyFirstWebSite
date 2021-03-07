using Microsoft.AspNet.Identity;
using MyWebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace MyWebSite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            var posts = db.Posts;
            var users = db.Users;
            ViewBag.Posts = posts;
            ViewBag.Users = users;
            return View();
        }

        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            post.PostTime = DateTime.Now;
            post.UserId = User.Identity.GetUserId();
            post.Reklama = false;
            var user = db.Users.Find(User.Identity.GetUserId());
            post.OwnerName = user.FirstName + " " + user.LastName;
            db.Posts.Add(post);
            db.SaveChanges();
            return Redirect("/Home/Wall");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePost(int Id)
        {
            var post = db.Posts.Find(Id);
            try
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                return Redirect("/Home/AdminPanel");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteMyPost(int Id)
        {
            var post = db.Posts.Find(Id);
            try
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                return Redirect("/Home/Wall");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(string Id)
        {
            var user = db.Users.Find(Id);
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Redirect("/Home/AdminPanel");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteComments(int Id)
        {
            var comment = db.Comments.Find(Id);
            try
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateReklama()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateReklama(Post post)
        {
            post.PostTime = DateTime.Now;
            post.Reklama = true;
            post.UserId = User.Identity.GetUserId();
            var user = db.Users.Find(User.Identity.GetUserId());
            post.OwnerName = user.FirstName + " " + user.LastName;
            db.Posts.Add(post);
            db.SaveChanges();
            return Redirect("/Home/Wall");
        }

        [HttpGet]
        public ActionResult GetComments(int PostId = 0)
        {
            var post = db.Posts.Find(PostId);
            ViewBag.Post = post;
            ViewBag.Id = PostId;
            List<Comment> allcomment = new List<Comment>();
            foreach (var i in db.Comments)
            {
                if (i.PostId == PostId)
                {
                    allcomment.Add(i);
                }
            }
            ViewBag.Comments = allcomment;
            return View();
        }

        [HttpPost]
        public ActionResult GetComments(Comment comment)
        {
            comment.CommentTime = DateTime.Now;
            var user = db.Users.Find(User.Identity.GetUserId());
            comment.OwnerName = user.FirstName + " " + user.LastName;
            comment.UserId = User.Identity.GetUserId();
            db.Comments.Add(comment);
            db.SaveChanges();
            List<Comment> allcomment = new List<Comment>();
            foreach (var i in db.Comments)
            {
                if (i.PostId == comment.PostId)
                {
                    allcomment.Add(i);
                }
            }
            ViewBag.Comments = allcomment;
            return View();
        }

        public ActionResult ShowAllComment(Comment allcomment)
        {
            return PartialView(allcomment);
        }

        [HttpGet]
        public ActionResult UserWall(string UserId)
        {
            var posts = db.Posts;
            ViewBag.Posts = posts;
            var users = db.Users;
            ViewBag.Users = users;
            ViewBag.UserId = UserId;
            return View();
        }

        public ViewResult EditPost(int Id)
        {
            Post post = db.Posts.Find(Id);
            return View(post);
        }

        [HttpPost]
        public ActionResult EditPost(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Wall");
        }

        public ViewResult EditComments(int Id)
        {
            Comment comment = db.Comments.Find(Id);
            return View(comment);
        }

        [HttpPost]
        public ActionResult EditComments(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetComments", new { PostId = comment.PostId });
        }

        public ActionResult Wall()
        {
            var posts = db.Posts;
            ViewBag.Posts = posts;
            return View();
        }

        public ActionResult People()
        {
            var users = db.Users;
            ViewBag.Users = users;
            return View();
        }

        public ActionResult Index()
        {
            var posts = db.Posts;
            ViewBag.Posts = posts;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}