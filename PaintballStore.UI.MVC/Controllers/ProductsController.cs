using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaintballStore.DATA.EF;
using PaintballStore.UI.MVC.Utilities;

namespace PaintballStore.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private PaintballEntities1 db = new PaintballEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Manufacturer).Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "ManufacturerName");
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProducTypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Description,ProductTypeId,ManufacturerId,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                string file = "noImage.png";
                if(productImage != null)
                {
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".jfif" };
                    if(goodExts.Contains(ext.ToLower()))
                    {
                        file = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products/");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                    }
                    else
                    {
                        file = "noImage.png";
                    }
                }
                product.ProductImage = file;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "ManufacturerName", product.ManufacturerId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProducTypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "ManufacturerName", product.ManufacturerId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProducTypeName", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Description,ProductTypeId,ManufacturerId,ProductImage")] Product product,HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                string file="noImage.png";
                if(productImage != null)
                {
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", "jfif" };
                    if(goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        if (product.ProductImage != null && product.ProductImage != "noImage.png" )
                        {
                            string path = Server.MapPath("~/Content/img/products");
                            ImageServices.Delete(path, product.ProductImage);
                        }
                    }
                }
                product.ProductImage = file;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "ManufacturerName", product.ManufacturerId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "ProducTypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            string path = Server.MapPath("~/Content/img/products");
            ImageServices.Delete(path, product.ProductImage);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
