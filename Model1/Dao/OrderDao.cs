using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1.Dao
{
    public class OrderDao
    {
        OnlineDbOrder db = null;
        public OrderDao()
        {
            db = new OnlineDbOrder();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public bool Update(Order entity)
        {
            try
            {
                var product = db.Orders.Find(entity.ID);
                product.Status = entity.Status;
                product.Status = false;
                entity.Status = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
    }
}
