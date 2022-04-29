using Model.Dao;
using Model1.EF;
using Model1.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1.Dao
{
    public class CartDao
    {
        OnlineDbOrder db = null;
        public CartDao()
        {
            db = new OnlineDbOrder();
        }

        public Order GetById(long id)
        {
            return db.Orders.Find(id);
        }
        public Order GetBycusid(long id)
        {
            return db.Orders.Find(id);
        }
        public Order GetId()
        {
            long id = new Order().ID;
            return db.Orders.Find(id);
        }
        public Order GetByCreatedby(string createdby, long id)
        {
            var user = new Userdao().ViewDetail1(id);
            return db.Orders.SingleOrDefault(x => x.CreatedBy.Contains(createdby)&& x.Status==true&&x.CusID==user.ID);
        }

        public List<CartViewModel> ListById(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            
            totalRecord = db.Orders.Where(x => x.CreatedBy.Contains(keyword)).Count();
            var model = (from a in db.Orders
                         join b in db.OrderDetails
                         on a.ID equals b.OrderID
                         join c in db.Products
                         on b.ProductID equals c.ID
                         join d in db.Users
                         on a.CusID equals d.ID
                         where a.ID == b.OrderID
                         where c.ID == b.ProductID
                         where d.ID == a.CusID
                         where a.CreatedBy.Contains(keyword)
                         where a.CreatedBy==c.CreateBy
                         where a.Status==true
                         select new
                         {
                             ID = a.ID,
                             orderName = b.OrderID,
                             proid=b.ProductID,
                             ProName = c.Name,
                             UsName = d.UserName,
                             CreateBy=c.CreateBy,
                             CreatedDate=a.CreatedDate,
                             ShipEmail= a.ShipEmail,
                             Phone=a.ShipPhone,
                         }).AsEnumerable().Select(x => new CartViewModel()
                         {
                             ID = x.ID,
                             Name = x.UsName,
                             proid=x.proid,
                             orderName = x.orderName,
                             ProName = x.ProName,
                             CreatedBy=x.CreateBy,
                             CreatedDate=x.CreatedDate,
                             ShipEmail=x.ShipEmail,
                             Phone=x.Phone,
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model.ToList();
        }

        public List<CartViewModel> ListAllPaging(long keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            totalRecord = db.Orders.Where(x => x.CusID==keyword).Count();
            var model = (from a in db.Orders
                         join b in db.OrderDetails
                         on a.ID equals b.OrderID
                         join c in db.Products
                         on b.ProductID equals c.ID
                         join d in db.Users
                         on a.CusID equals d.ID
                         where a.ID == b.OrderID
                         where c.ID == b.ProductID
                         where d.ID == a.CusID
                         where a.CusID==keyword
                         where a.CreatedBy == c.CreateBy                        
                         select new
                         {
                             ID = a.ID,
                             orderName = b.OrderID,
                             proid = b.ProductID,
                             ProName = c.Name,
                             UsName = d.UserName,
                             CreateBy = c.CreateBy,
                             CreatedDate = a.CreatedDate,
                             status=a.Status,
                             CusID=a.CusID
                         }).AsEnumerable().Select(x => new CartViewModel()
                         {
                             ID = x.ID,
                             Name = x.UsName,
                             proid = x.proid,
                             orderName = x.orderName,
                             ProName = x.ProName,
                             CreatedBy = x.CreateBy,
                             CreatedDate = x.CreatedDate,
                             Status = (bool)x.status,
                             CusID = (long)x.CusID
                         });
            model.OrderBy(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model.ToList();
        }
    }
}
