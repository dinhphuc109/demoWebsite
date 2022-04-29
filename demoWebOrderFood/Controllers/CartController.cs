using Antlr.Runtime;
using BotDetect;
using Common;
using demoWebOrderFood.Common;
using demoWebOrderFood.Models;
using Model.Dao;
using Model1.Dao;
using Model1.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Xml.Schema;

namespace demoWebOrderFood.Controllers
{
    public class CartController : Controller
    {
        OnlineDbOrder db = null;
        public CartController()
        {
            db = new OnlineDbOrder();
        }
        private const string CartSession = "CartSesstion";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];

            if (cart != null)
            {
                
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product == product)
                        {
                            item.Quantity += quantity;
                            
                        }

                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GiveAndReceive()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult GiveAndReceive(Order model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            //var cartdao = new CartDao().GetById(id);

            //var oderId = new CartDao().GetById(oderid);
            var total= db.Orders.Where(x => x.CusID==session.UserID && x.Status==true).Count();

            if (total>=1) {
                Content("<script language='javascript' type='text/javascript'>alert('You already have an item, please wait for confirmation');</script>");
                return Redirect("/success");
                
            }
            else
            {
                var oderdetail = new OrderDetail();
                var user = new Userdao().GetById(session.UserName);
                //order.CreatedDate = DateTime.Now;
                //order.CustomerID = user.ID;
                //var orderDetail = new OrderDetail();
                //orderDetail.ProductID = product.ID;
                //orderDetail.OrderID = oderId.ID;

                model.CreatedDate = DateTime.Now;
                model.CusID = session.UserID;
                model.ShipName = user.Name;
                model.ShipPhone = user.Phone;
                model.ShipAddress = user.Address;
                model.ShipEmail = user.Email;
                model.Status = true;
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new Model1.Dao.OrderDetailDao();
                foreach (var item in cart)
                {
                    model.CreatedBy = item.Product.CreateBy;
                }
                var id = new OrderDao().Insert(model);
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                }

                cart.Clear();
                Content("<script language='javascript' type='text/javascript'>alert('success for register the item');</script>");
                return Redirect("/success");
            }            
        }

        
        

        public ActionResult Success(int page = 1, int pageSize = 10)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                int totalRecord = 0;
                var model = new CartDao().ListAllPaging(session.UserID, ref totalRecord, page, pageSize);
                long idd = new User().ID;
                var user = new Userdao().ViewDetail1(idd);

                ViewBag.Total = totalRecord;
                ViewBag.pageIndex = page;
                ViewBag.id = session.UserID;
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

        [ChildActionOnly]
        public PartialViewResult HeaderGive()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        
    }
}