using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RevenueReport
    {
        public string MachineName { get; set; }      // Tên máy
        public DateTime StartTime { get; set; }      // Thời gian bắt đầu phiên
        public DateTime EndTime { get; set; }        // Thời gian kết thúc phiên
        public decimal TotalAmount { get; set; }    // Tổng tiền máy từ MachineSessions (tính giờ)
        public decimal Price { get; set; }           // Giá từ bảng Orders
        public decimal SumBill { get; set; }         // Tổng doanh thu (ToltalAmount + Price)
    }
}
