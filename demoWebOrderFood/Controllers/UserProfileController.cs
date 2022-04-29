using demoWebOrderFood.Common;
using Model.Dao;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace demoWebOrderFood.Controllers
{
    
    public class UserProfileController : Controller
    {
      

        // GET: UserProfile

        public ActionResult Index()
        {
            
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
                return Redirect("/");
            else
            {
                var username = session.UserName;
                var model = new Userdao().profileUser(username);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new Userdao();
            var user = dao.ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var dao = new Userdao();

                var result = dao.Update(model);
                if (result)
                {
                    return Redirect("/user-profile");
                }
                else
                {
                    ModelState.AddModelError("", "update successfully!!!");
                }
            }
            return View(model);
        }


        public ActionResult HistoryOrder(int page = 1, int pageSize = 8)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var username = session.UserName;
            var dao = new ProductDao();
            var model = dao.ListAll(username, page, pageSize);      
            return View(model);
        }
    }
}