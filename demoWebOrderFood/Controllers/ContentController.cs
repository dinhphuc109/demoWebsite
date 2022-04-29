using demoWebOrderFood.Common;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWebOrderFood.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            string markers = "[";
            string conString = ConfigurationManager.ConnectionStrings["OnlineDbOrder2"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Content");
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["ViewCount"].Equals(400))
                        {
                            markers += "{";
                            markers += string.Format("'title': '{0}',", sdr["Name"]);
                            markers += string.Format("'lat': '{0}',", sdr["Lat"]);
                            markers += string.Format("'lng': '{0}',", sdr["Lng"]);
                            markers += string.Format("'description': '{0}'", sdr["Image"]);
                            markers += "},";
                        }
                        
                    }
                }
                con.Close();
            }

            markers += "];";
            ViewBag.Markers = markers;
            var productdao = new ContentDao();
            var model = productdao.ListAllPaging(searchString, page, pageSize);            
            ViewBag.SearchString = searchString;
            ViewBag.SearchString2 = searchString;
            return View(model);
        }

        public ActionResult IndexMaps(string searchString2, int page2 = 1, int pageSize2 = 4)
        {
            string markers = "[";
            string conString = ConfigurationManager.ConnectionStrings["OnlineDbOrder2"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Content");
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["ViewCount"].Equals(400))
                        {
                            markers += "{";
                            markers += string.Format("'title': '{0}',", sdr["Name"]);
                            markers += string.Format("'lat': '{0}',", sdr["Lat"]);
                            markers += string.Format("'lng': '{0}',", sdr["Lng"]);
                            markers += string.Format("'description': '{0}'", sdr["Image"]);
                            markers += "},";
                        }

                    }
                }
                con.Close();
            }

            markers += "];";
            ViewBag.Markers = markers;
            var productdao = new ContentDao();           
                var model = productdao.ListAllMaps(searchString2, page2, pageSize2);
                ViewBag.SearchString2 = searchString2;
                return View(model);         
        }

        public ActionResult Detail(long id)
        {
            var model = new ContentDao().GetByID(id);

            ViewBag.Tags = new ContentDao().ListTag(id);
            return View(model);
        }

        public ActionResult Tag(string tagId, int page = 1, int pageSize = 8)
        {
            var model = new ContentDao().ListAllByTag(tagId, page, pageSize);
            int totalRecord = 0;

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            ViewBag.Tag = new ContentDao().GetTag(tagId);
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

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.MetaKeywords = "11";
                model.MetaDescriptions = "11";
                model.CategoryID = 1;
                if (model.Tags == null)
                    model.Tags = "Khác";
                model.Status = true;
                model.CreatedBy = session.UserName;
                new ContentDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        public JsonResult ListName(string q)
        {
            var data = new ContentDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 10)
        {

            int totalRecord = 0;
            var model = new ContentDao().Search(keyword, ref totalRecord, page, pageSize);

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