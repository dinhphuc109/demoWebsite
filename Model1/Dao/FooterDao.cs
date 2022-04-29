
using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1.Dao
{
    public class FooterDao
    {
        OnlineDbOrder db = null;
        public FooterDao()
        {
            db = new OnlineDbOrder();
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == false);
        }
    }
}
