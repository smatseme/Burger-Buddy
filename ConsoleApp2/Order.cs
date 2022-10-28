using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Order
    {
        public int OrderID { get; set; }        
        public DateTime OrderTimeCreated { get; set; }
        public bool IsPOS { get; set; }
        public List<MenuItem> ItemsOrdered { get; set; }

        //public Order(int _OrderID,DateTime _OrderTimeCreated,bool _IsPOS,List<MenuItem> _ItemsOrdered)
        //{
        //    OrderID = _OrderID;
        //    OrderTimeCreated = _OrderTimeCreated;
        //    IsPOS = _IsPOS;
        //    ItemsOrdered = _ItemsOrdered;

        //}
    }
}
