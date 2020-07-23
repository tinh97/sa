using Model.Dao;
using shopee_tedu.Areas.Admin.Common;
using shopee_tedu.Areas.Admin.Data;
using shopee_tedu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopee_tedu.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash( model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var userLogin = new UserLogin();
                    userLogin.UserID = user.ID;
                    userLogin.UserName = user.UserName;
                    Session.Add(CommonConstants.USER_SESSION, userLogin);
                    return RedirectToAction("Index", "Home");

                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản chưa được active");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View("Index");
        }
    }
}