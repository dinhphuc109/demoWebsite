
using demoWebOrderFood.Common;
using demoWebOrderFood.Models;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace demoWebOrderFood.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            var productdao = new ProductDao();
            ViewBag.ListFeatureProducts = productdao.ListFeatureProduct(8);
            return View();
        }

        [ChildActionOnly]
        public ActionResult HotNews()
        {
            var contentdao = new ContentDao();
            var model  = contentdao.ListHotNews(3);
            return View(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration =3600*24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult LoginMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult SubMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult SubMenu2()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }


        


        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }

  
    }
}