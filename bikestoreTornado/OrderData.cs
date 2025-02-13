using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikestoreTornado
{
    public class OrderData
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int BikeID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public OrderData(string customerName, int bikeID, decimal unitPrice, int quantity)
        {
            OrderDate = DateTime.Now.Date;
            CustomerName = customerName;
            BikeID = bikeID;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}

    
