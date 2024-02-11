using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    img = img.OrderByDescending(p => p.img_date);
                    break;
                default:
                    img = img.OrderBy(p => p.img_id);
                    break;
            }
            return View(img);
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
        public ActionResult Details(int id)
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
    }
}