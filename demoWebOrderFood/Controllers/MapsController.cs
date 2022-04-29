using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWebOrderFood.Controllers
{
    public class MapsController : Controller
    {
        // GET: Maps
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Maps(string searchString, int page = 1, int pageSize = 8)
        {
            var productdao = new ContentDao();
            var model = productdao.ListAllMaps(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Map model)
        {
            if (ModelState.IsValid)
            {
                
                
                model.Status = true;
                
                new MapsDao().Create(model);
            }
            return Redirect("/");
        }
    }
}