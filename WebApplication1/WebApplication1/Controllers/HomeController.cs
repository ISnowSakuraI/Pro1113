using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Entities db = new Entities();
        public ViewResult Index(string sortImg,string searchString)
        {
            ViewBag.ImgSortParm = String.IsNullOrEmpty(sortImg) ? "img_desc" : "";

            var img = from p in db.Imgs
                select p;
    
            if (!String.IsNullOrEmpty(searchString))
            {
                img = img.Where(p => p.img_name.Contains(searchString)
                                    || p.img_category.Contains(searchString));
            }
            switch (sortImg)
            {
                case "img_desc":
                    img = img.OrderBy(p => p.img_name);
                    break;
                default:
                    img = img.OrderBy(p => p.img_id);
                    break;
            }
            return View(img);
        }

        public ActionResult MyGallery()
        {
            ViewBag.Message = "Your application description page.";

            return View(db.Imgs.ToList().Where(x => x.UserName == User.Identity.Name));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "img_id,img_name,img_description,img_category,UserName,img_url,img_date")] Img img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(img);
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            List<Comment> comments = db.Comments.Where(r => r.img_id == id).ToList();

            var viewModel = new CommentViewModel
            {
                img = img,
                comments = comments
            };   
            return View(viewModel);
        }
        public ActionResult MyDetails(int id)
        {
            var dataId = db.Imgs.Single(x => x.img_id == id);
            return View(dataId);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Img img)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                        file.SaveAs(path);
                        img.img_url = fileName;
                    }
                }
                img.UserName = User.Identity.Name;
                img.img_date = DateTime.Now;
                db.Imgs.Add(img);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Img img = db.Imgs.Find(id);
            db.Imgs.Remove(img);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Comment(int img_id, string comment)
        {
            var CM = new Comment
            {
                img_id = img_id,
                UserName = User.Identity.Name,
                comment1 = comment,
                comment_date = DateTime.Now
            };
            db.Comments.Add(CM);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = img_id });
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        public JsonResult GetReportJson()
        {
            var data = db.Imgs.ToList();
            return Json(new { JSONList = data }, JsonRequestBehavior.AllowGet);
        }
    }
}