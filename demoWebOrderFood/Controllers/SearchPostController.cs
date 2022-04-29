using demoWebOrderFood.Common;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWebOrderFood.Controllers
{
    public class SearchPostController : Controller
    {
        // GET: SearchPost
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchPost(string searchString, int page = 1, int pageSize = 8)
        {
            int totalRecord = 0;
            var post = new SearchPost();
            var productdao = new SearchPostDao();
            var model = productdao.ListAllPaging(searchString, page, pageSize);
            ViewBag.model = new ProductDao().Search(post.Keyword, ref totalRecord, page, pageSize);
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
        public ActionResult Create(SearchPost model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedDay = DateTime.Now;
                model.Status = true;
                model.CreatedBy = session.UserName;
                new SearchPostDao().Create(model);
            }
            return Redirect("/Trade/Index");
        }

        public ActionResult ReSearchPost(string keyword, int page = 1, int pageSize = 10)
        {            
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.pageIndex = page;
            ViewBag.keyword = keyword;
            int maxpage = 5;
            int totalpage = 0;
            totalpage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalpage;
            ViewBag.MaxPage = maxpage;
            ViewBag.First = 1;
            ViewBag.Last = totalpage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}