using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopee_tedu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            ViewBag.TrangSuc = productDao.TrangSucProduct(1);
            ViewBag.NuocHoa = productDao.NuocHoaProduct(1);
            var model = productDao.ListProduct(2);
            return View(model);
        }
        public ActionResult Contact()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult MenuBottomRight()
        {
            var model = new MenuDao().ListMenuByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult subcate()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult FooterPartial()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }

        


    }
}