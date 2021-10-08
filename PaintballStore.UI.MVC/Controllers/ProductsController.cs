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


        public ActionResult Shop()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        public ActionResult Masks()
        {
            var masks = db.Products.Where(x => x.ProductType.ProducTypeName.ToLower() == "masks").ToList();
            return View(masks);
        }

        public ActionResult Apparel()
        {
            var apparel = db.Products.Where(x => x.ProductType.ProducTypeName.ToLower() == "apparel").ToList();
            return View(apparel);
        }

        public ActionResult AirTanks()
        {
            var tanks = db.Products.Where(x => x.ProductType.ProducTypeName.ToLower() == "air tank").ToList();
            return View(tanks);
        }

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
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Description,ProductTypeId,ManufacturerId,ProductImage,ProductImage2,ProductImage3")] Product product, HttpPostedFileBase productImage,HttpPostedFileBase productImage2, HttpPostedFileBase productImage3)
        {
            if (ModelState.IsValid)
            {
                string file = "noImage.png";
                string fileTwo = "noImage.png";
                string fileThree = "noImage.png";
                //string file2 = "noImage.png";
                //string file3 = "noImage.png";
                if (productImage != null)
                {
                    file = productImage.FileName;

                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".jfif" };
                    if (goodExts.Contains(ext.ToLower()))
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


                if (productImage2 != null)
                {
                    fileTwo = productImage2.FileName;

                    string ext = fileTwo.Substring(fileTwo.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".jfif" };
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        fileTwo = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products/");
                        Image convertedImage = Image.FromStream(productImage2.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, fileTwo, convertedImage, maxImageSize, maxThumbSize);
                    }
                    else
                    {
                        file = "noImage.png";
                    }
                }

                if (productImage3 != null)
                {
                    fileThree = productImage3.FileName;

                    string ext = fileThree.Substring(fileThree.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".jfif" };
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        fileThree = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products/");
                        Image convertedImage = Image.FromStream(productImage3.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, fileThree, convertedImage, maxImageSize, maxThumbSize);
                    }
                    else
                    {
                        file = "noImage.png";
                    }
                }

                product.ProductImage = file;
                product.ProductImage2 = fileTwo;
                product.ProductImage3 = fileThree;
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
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Description,ProductTypeId,ManufacturerId,ProductImage,ProductImage2,ProductImage3")] Product product, HttpPostedFileBase productImage,HttpPostedFileBase productImage2, HttpPostedFileBase productImage3)
        {
            if (ModelState.IsValid)
            {
                string file = "noImage.png";
                string fileTwo = "noImage.png";
                string fileThree = "noImage.png";
                if (productImage != null)
                {
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", "jfif" };
                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        if (product.ProductImage != null && product.ProductImage != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/img/products");
                            ImageServices.Delete(path, product.ProductImage);
                        }
                    }
                }
                if (productImage2 != null)
                {
                    fileTwo = productImage2.FileName;
                    string ext = fileTwo.Substring(fileTwo.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", "jfif" };
                    if (goodExts.Contains(ext.ToLower()) && productImage2.ContentLength <= 4194304)
                    {
                        fileTwo = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products");
                        Image convertedImage = Image.FromStream(productImage2.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, fileTwo, convertedImage, maxImageSize, maxThumbSize);
                        if (product.ProductImage2 != null && product.ProductImage2 != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/img/products");
                            ImageServices.Delete(path, product.ProductImage2);
                        }
                    }
                }
                if (productImage3 != null)
                {
                    fileThree = productImage3.FileName;
                    string ext = fileThree.Substring(fileThree.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", "jfif" };
                    if (goodExts.Contains(ext.ToLower()) && productImage3.ContentLength <= 4194304)
                    {
                        fileThree = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/img/products");
                        Image convertedImage = Image.FromStream(productImage3.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageServices.ResizeImage(savePath, fileThree, convertedImage, maxImageSize, maxThumbSize);
                        if (product.ProductImage3 != null && product.ProductImage3 != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/img/products");
                            ImageServices.Delete(path, product.ProductImage3);
                        }
                    }
                }
                product.ProductImage = file;
                product.ProductImage2 = fileTwo;
                product.ProductImage3 = fileThree;
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
