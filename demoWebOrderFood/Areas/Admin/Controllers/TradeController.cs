using demoWebOrderFood.Common;
using Model.Dao;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWebOrderFood.Areas.Admin.Controllers
{
    public class TradeController : BaseController
    {
        // GET: Admin/Trade
        public ActionResult Index(int page = 1, int pageSize = 20)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                int totalRecord = 0;
                var model = new CartDao().ListById(session.UserName, ref totalRecord, page, pageSize);
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
    }
}