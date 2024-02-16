using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ImgsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Imgs
        public IQueryable<Img> GetImgs()
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            var prod = db.Imgs.ToList();
            foreach(var item in prod)
            {
                item.img_url = url + "/Img/" + item.img_url;
            }
            return db.Imgs;
        }

        // GET: api/Imgs/5
        [ResponseType(typeof(Img))]
        public IHttpActionResult GetImg(int id)
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return NotFound();
            }
            img.img_url = url + "/Img/" + img.img_url;


            return Ok(img);
        }

        // PUT: api/Imgs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImg(int id, Img img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != img.img_id)
            {
                return BadRequest();
            }

            db.Entry(img).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImgExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Imgs
        [ResponseType(typeof(Img))]
        public IHttpActionResult PostImg(Img img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Imgs.Add(img);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = img.img_id }, img);
        }

        // DELETE: api/Imgs/5
        [ResponseType(typeof(Img))]
        public IHttpActionResult DeleteImg(int id)
        {
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return NotFound();
            }

            db.Imgs.Remove(img);
            db.SaveChanges();

            return Ok(img);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImgExists(int id)
        {
            return db.Imgs.Count(e => e.img_id == id) > 0;
        }
    }
}