using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopee_tedu.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult productDetail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelateProduct = new ProductDao().ListRelatedProducts(id, 5);
            var model = new ProductDao().ViewDetail(id);
            return View(model);
        }

        //public ActionResult ViewAll()
        //{
        //    var model = new ProductDao().ListNewProduct(6);
        //    return View(model);
        //}

        public ActionResult GetProductByCategory(long id, int page = 1, int pageSize = 1)
        {
            
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(id, ref totalRecord, page, pageSize);
            ViewBag.NumberOfCategory = model.Count;
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        //public ActionResult GetRelateProduct(long productId)
        //{
        //    ViewBag.RelateProduct = new ProductDao().ListRelatedProducts(productId, 2);
        //    return View();
        //}

        //public ActionResult GetCategoryName(long productId)
        //{
        //    var product = new ProductDao().ViewDetail(productId);
        //    ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
        //    return View();
        //}
    }
}