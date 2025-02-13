using System;

namespace bikestoreTornado
{
    public class OrderData
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int BikeID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderData(DateTime orderDate, string customerName, int bikeID, decimal unitPrice, decimal totalPrice)
        {
            OrderDate = orderDate;
            CustomerName = customerName;
            BikeID = bikeID;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
        }
    }
}