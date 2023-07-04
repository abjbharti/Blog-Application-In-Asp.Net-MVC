using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AbhijeetMvcTestBlogApplication.Models;
using PagedList;

namespace FirstBlog1.Controllers
{
    [Authorize(Roles = "user")]
    public class BlogsController : Controller
    {
        private readonly BlogDbContext context = new BlogDbContext();

        // GET: Blogs
        public ActionResult Index()
        {
            Guid Id1 = (Guid)Session["Id"];
            var userData = (from user in context.Blogs
                            where user.IdUser == Id1
                            select user).ToList();

            return View(userData);
        }

        public ActionResult Home(string search, int? page)
        {
            return View(context.Blogs.Where(x => x.Title.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 5));
        }

        // GET: Blogs/Details
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

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Blog blog)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);

            string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
            blog.Image = "~/Content/Images/" + _filename;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength < 100000)
                {
                    blog.IdUser = (Guid)Session["Id"];
                    blog.IsEnabled = true;
                    context.Blogs.Add(blog);
                    context.SaveChanges();

                    file.SaveAs(path);
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "Select lower size image";
                }
            }
            else
            {
                ViewBag.msg = "Invalid File Type";
            }
            return View();

        }

        // GET: Blogs/Edit
        public ActionResult Edit(int? id)
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
            Session["ImgPath"] = blog.Image;

            return View(blog);
        }


        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.IdUser = (Guid)Session["Id"];
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);

                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
                    blog.Image = "~/Content/Images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength < 100000)
                        {
                            string OldImgPath = Request.MapPath(Session["ImgPath"].ToString());
                            context.Entry(blog).State = EntityState.Modified;
                            context.SaveChanges();

                            file.SaveAs(path);
                            if (System.IO.File.Exists(OldImgPath))
                            {
                                System.IO.File.Delete(OldImgPath);
                            }
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ViewBag.msg = "Select lower size image";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Invalid File Type";
                    }
                }
                else
                {
                    blog.Image = Session["ImgPath"].ToString();
                    context.Entry(blog).State = EntityState.Modified;
                    if (context.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(blog);
        }




        // GET: Blogs/Delete
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

        // POST: Blogs/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = context.Blogs.Find(id);
            string imagePath = blog.Image; // Get the image path from the blog post
            context.Blogs.Remove(blog);
            context.SaveChanges();

            // Delete the image file from the server
            if (System.IO.File.Exists(Server.MapPath(imagePath)))
            {
                System.IO.File.Delete(Server.MapPath(imagePath));
            }

            return RedirectToAction("Index");
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