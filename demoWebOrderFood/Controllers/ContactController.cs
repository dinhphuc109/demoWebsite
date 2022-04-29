using Common;
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
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }

        [HttpPost]
        public JsonResult Send( string name, string phone, string address, string email, string message)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreateDate = DateTime.Now;
            feedback.Phone = phone;
            feedback.Content = message;
            feedback.Address = address;
            
           

            var id = new ContactDao().InsertFeedback(feedback);
            if (id > 0)
            {
                try
                {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/Template/newMail.html"));
                    
                    content = content.Replace("{{CustomerName}}", name);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{Phone}}", phone);
                    content = content.Replace("{{Address}}", address);
                    content = content.Replace("{{Message}}", message);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();                   
                    new MailHelper().SendMail(toEmail, "ECB wellcome", content);
                }
                catch (Exception ex)
                {
                    //ghi log
                    Redirect("/loi-gui-mail");
                }
                Redirect("/hoan-thanh");
                return Json(new
                {
                    status = true
                });
                
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}