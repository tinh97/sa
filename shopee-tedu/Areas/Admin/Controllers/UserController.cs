using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using shopee_tedu.Areas.Admin.Common;

namespace shopee_tedu.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
       
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)// page là trang sẽ xuất hiện đầu tiên khi load, pageSize là số bản ghi trên một trang
        {
            var dao = new UserDao();
            IEnumerable<User> model = dao.GetListPagging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                long id = dao.Insert(user);
                if(id > 0)
                {
                    SetAlert("thêm mới thành công !", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "them moi k thanh cong");
                }
            }
            return View("Create");// nếu form điền chưa thỏa mãn thì trả về trang thêm mới để điền lại
        }
        [HttpGet]
        public ActionResult Update1()
        {
           
            return View();
        }



        public ActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedPass = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedPass;
                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("sửa thành công !", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "cap nhat k thanh cong");
                }
            }
            return View("Index");
        }


        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            new UserDao().Deleteuser(id);
            return RedirectToAction("Index");
        }

        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}