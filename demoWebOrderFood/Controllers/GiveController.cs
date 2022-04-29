using Common;
using demoWebOrderFood.Common;
using Model.Dao;
using Model1.Dao;
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWebOrderFood.Controllers
{
    public class GiveController : Controller
    {
        // GET: Give
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

        [HttpPost]
        public ActionResult Give(string productname, string name, string mobile, string address, string email, string message, Order model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreateDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = message;
            feedback.Address = address;
            feedback.Status = true;
            var userd = new User();
            var idd = new ContactDao().InsertFeedback(feedback);
            if (idd > 0)
            {
                if (name == model.ShipName)
                {
                    model.ID = int.Parse(address);
                    model.CreatedBy = session.UserName;
                    model.Status = false;
                    var dao = new OrderDao();
                    var result = dao.Update(model);
                }
                else
                {
                    model.ID = int.Parse(address);
                    model.CreatedBy = session.UserName;
                    model.Status = false;
                    var dao = new OrderDao();
                    var result = dao.Update(model);
                }
                try
                {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/Template/ordermail.html"));
                    
                    content = content.Replace("{{CustomerName}}", name);
                    content = content.Replace("{{Phone}}", mobile);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{Address}}", address);
                    content = content.Replace("{{Message}}", message);
                    content = content.Replace("{{ProductName}}", productname);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    new MailHelper().SendMail(email, "ECB.Team send", content);
                    new MailHelper().SendMail(toEmail, "ECB.Team send", content);
                }
                catch (Exception ex)
                {
                    //ghi log
                    Redirect("/loi-gui-mail");
                }
                Redirect("/hoan-thanh");
                Content("<script language='javascript' type='text/javascript'>alert('Success');</script>");
                return RedirectToAction("/Index");
            }

            else
                return RedirectToAction("/Index");
        }
    }
}