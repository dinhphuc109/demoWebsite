using demoWebOrderFood.Common;
using demoWebOrderFood.Models;
using Model.Dao;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWebOrderFood.Controllers
{
    public class TradeController : Controller
    {
        private const string CartSession = "CartSesstion";
        // GET: Trade
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var productdao = new ProductDao();
            var model = productdao.ListAllPaging(searchString, page, pageSize);
            ViewBag.NewProducts = productdao.ListNewProduct(10);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategoryMb()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Category(long id, int page = 1, int pageSize = 10)
        {
            var category = new ProductCategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(id,ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.pageIndex = page;
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

        public ActionResult Search(string keyword, int page = 1, int pageSize = 10)
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

        //public ActionResult AddItem(long productId, int quantity)
        //{
        //    var product = new ProductDao().ViewDetail(productId);
        //    var cart = Session[CartSession];

        //    if (cart != null)
        //    {

        //        var list = (List<CartItem>)cart;
        //        if (list.Exists(x => x.Product.ID == productId))
        //        {
        //            foreach (var item in list)
        //            {
        //                if (item.Product == product)
        //                {
        //                    item.Quantity += quantity;

        //                }

        //            }
        //        }
        //        else
        //        {
        //            var item = new CartItem();
        //            item.Product = product;
        //            item.Quantity = quantity;
        //            list.Add(item);
        //        }
        //        Session[CartSession] = list;
        //    }
        //    else
        //    {
        //        //tạo mới đối tượng cart item
        //        var item = new CartItem();
        //        item.Product = product;
        //        item.Quantity = quantity;
        //        var list = new List<CartItem>();
        //        list.Add(item);
        //        //Gán vào session
        //        Session[CartSession] = list;
        //    }
        //    return Redirect("/Cart/Index");
        //}

        [OutputCache(CacheProfile = "cache1product")]
        public ActionResult Detail(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.relatedProducts = new ProductDao().Listrelatedproducts(id);
            var sessionproduct = new User();
            var model = new CartDao().GetId();

            ViewBag.setId = model;
                var cart = Session[CartSession];
                var list = new List<CartItem>();
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                }
                ViewBag.sessioncart = list;
            
            
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.MetaKeyword = "11";
                model.MetaDesciptions = "11";
                model.Status = true;
                model.CreateBy = session.UserName;
                new ProductDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        
        
    }
}