using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1.ViewModel
{
    public class CartViewModel
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public long orderName { set; get; }
        public long proid { set; get; }
        public string ProName { set; get; }
        public string UsName { set; get; }
        public string CreatedBy { set; get; }
        public string Phone { set; get; }
        public string ShipEmail { set; get; }       
        public DateTime? CreatedDate { set; get; }
        public bool Status { get; set; }
        public long CusID { set; get; }
    }
}
