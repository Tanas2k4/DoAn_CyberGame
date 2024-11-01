using DAL.Entities; // Đảm bảo bạn đã thêm namespace chứa các thực thể của DAL
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace BUS
{
    public class ServiceService
    {
        private readonly CyBerModel context; // Đối tượng DbContext của bạn
        private readonly MachineService machineService; // Tham chiếu đến MachineService

        public ServiceService()
        {
            context = new CyBerModel(); // Khởi tạo DbContext
            machineService= new MachineService(); // Khởi tạo MachineService
        }

        // Lấy tất cả món ăn
        public List<FoodItem> GetFoodItems()
        {
            return context.FoodItems.AsNoTracking().ToList();
        }

        // Phương thức tạo đơn hàng
        public void CreateOrder(int sessionId, int foodItemId, int quantity, decimal price)
        {
            // Kiểm tra đầu vào có hợp lệ không
            if (sessionId <= 0 || foodItemId <= 0 || quantity <= 0 || price <= 0)
            {
                throw new ArgumentException("Các giá trị đầu vào không hợp lệ.");
            }

            // Kiểm tra sessionId có tồn tại không
            var sessionExists = context.MachineSessions.Any(s => s.SessionId == sessionId);
            if (!sessionExists)
            {
                throw new Exception("Session không tồn tại.");
            }

            // Kiểm tra foodItemId có tồn tại không
            var foodItemExists = context.FoodItems.Any(f => f.FoodItemId == foodItemId);
            if (!foodItemExists)
            {
                throw new Exception("Món ăn không tồn tại.");
            }

            // Tạo đơn hàng mới nếu tất cả các điều kiện đều hợp lệ
            var order = new Order
            {
                SessionId = sessionId,
                FoodItemId = foodItemId,
                Quantity = quantity,
                Price = price
            };

            context.Orders.Add(order); // Thêm đơn hàng mới vào DbContext
            context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }
        public List<string> GetFoodItemNamesBySessionId(int sessionId)
        {
            using (var context = new CyBerModel())
            {
                return context.Orders
                    .Where(o => o.SessionId == sessionId)
                    .Select(o => o.FoodItem.Name)
                    .ToList();
            }
        }
        public FoodItem GetFoodItemById(int foodItemId)
        {
            return context.FoodItems.FirstOrDefault(fi => fi.FoodItemId == foodItemId);
        }



        public List<Order> GetOrdersForMachine(int machineId)
        {
            // Lấy danh sách đơn hàng cho máy dựa vào machineId
            return context.Orders
                           .Where(o => o.MachineSession.MachineId == machineId)
                           .ToList();
        }


        public void SaveOrder(Order order)
        {
            try
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi chi tiết
                Console.WriteLine($"Lỗi khi lưu đơn hàng: {ex.Message}");
                throw; // Ném lại ngoại lệ để xử lý ở nơi gọi
            }
        }

        public List<Order> GetOrdersForSession(int sessionId)
        {
            return context.Orders.Where(o => o.SessionId == sessionId).Include(o => o.FoodItem).ToList();
        }

        // Lấy danh sách loại món ăn (Foodroles)
        public List<string> GetFoodRoles()
        {
            return context.FoodItems
                          .Select(f => f.Foodroles)
                          .Distinct()
                          .ToList();
        }
        public List<FoodItem> GetFoodItemsByRole(string role)
        {
            return context.FoodItems
                            .Where(fi => fi.Foodroles.Equals(role))
                            .ToList();
        }



        /////////////THÊM XÓA SỬA///////////////

        //THÊM
        public void AddFoodItem(FoodItem foodItem)
        {
            context.FoodItems.Add(foodItem);
            context.SaveChanges(); // Lưu thay đổi vào database
        }


        //SỬA
        public void UpdateFoodItem(FoodItem foodItem)
        {
            var existingFoodItem = context.FoodItems.Find(foodItem.FoodItemId);
            if (existingFoodItem != null)
            {
                existingFoodItem.Name = foodItem.Name;
                existingFoodItem.Price = foodItem.Price;
                existingFoodItem.Foodroles = foodItem.Foodroles;
                existingFoodItem.ImagePath = foodItem.ImagePath;
                context.Entry(existingFoodItem).State = EntityState.Modified;
                context.SaveChanges(); // Lưu thay đổi vào database
            }
        }


        //XÓA
        public void DeleteFoodItem(int foodItemId)
        {
            var foodItem = context.FoodItems.Find(foodItemId);
            if (foodItem != null)
            {
                context.FoodItems.Remove(foodItem);
                context.SaveChanges(); // Lưu thay đổi vào database
            }
        }
    }
}
