using AbhijeetMvcTestBlogApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace AbhijeetMvcTestBlogApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private BlogDbContext context = new BlogDbContext();
        // GET: Admin
        public ActionResult Index(string search, int? page)
        {
            var blogs = from b in context.Blogs
                        where b.IsEnabled != false
                        select b;
            return View(blogs.Where(x => x.Title.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 5));
        }

        // GET: Admin/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = context.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }


        // GET: Admin/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = context.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Admin/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = context.Blogs.Find(id);
            context.Blogs.Remove(blog);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Enable(int? id)
        {
            Blog blog = context.Blogs.Find(id);
            blog.IsEnabled = true;
            context.Entry(blog).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("DisabledPosts", "Admin");
        }

        public ActionResult Disable(int? id)
        {
            Blog blog = context.Blogs.Find(id);
            blog.IsEnabled = false;
            context.Entry(blog).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DisabledPosts()
        {
            var blogs = from b in context.Blogs
                        where b.IsEnabled == false
                        select b;

            return View(blogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}