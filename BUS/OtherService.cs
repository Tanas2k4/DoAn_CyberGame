using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BUS
{
    public class OtherService 
    { 
        public int OrderId { get; set; }
        
        public int SessionId { get; set; } 
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public static List<OtherService> ConvertFromOrders(List<Order> orders) 
        { 
            return orders.Select(order => new OtherService 
            { 
                OrderId = order.OrderId,
                SessionId = order.SessionId,
                FoodItemId = order.FoodItemId,
                Quantity = order.Quantity,
                Price = order.Price }
            ).ToList(); 
        } 
    }
}
