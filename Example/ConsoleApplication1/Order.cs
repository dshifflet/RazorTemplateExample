using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngine
{
    public class Order
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ID { get; set; }
        public List<OrderLineItem> LineItems { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
    }

    public class OrderLineItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        
    }
}
