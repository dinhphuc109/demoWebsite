using Model1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoWebOrderFood.Models
{
    [Serializable]
    public class CartItem
    {
        
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}