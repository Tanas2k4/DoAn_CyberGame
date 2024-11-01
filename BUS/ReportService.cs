using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ReportService
    {
        private CyBerModel context;

        public ReportService()
        {
            context = new CyBerModel();
        }

       

        public List<RevenueReport> GetRevenueReport()
{
            using (var context = new CyBerModel())
            {
                var reportData = context.MachineSessions
                    .Select(session => new RevenueReport
                    {
                        MachineName = session.Machine.Name,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime ?? DateTime.Now,
                        TotalAmount = session.TotalAmount ?? 0m,
                        Price = context.Orders
                            .Where(o => o.SessionId == session.SessionId)
                            .Sum(o => (decimal?)o.Price * o.Quantity) ?? 0m,
                        SumBill = (session.TotalAmount ?? 0m) +
                                   (context.Orders.Where(o => o.SessionId == session.SessionId)
                                   .Sum(o => (decimal?)o.Price * o.Quantity) ?? 0m)
                    }).ToList();

                // In ra console để kiểm tra dữ liệu
                foreach (var item in reportData)
                {
                    Console.WriteLine($"Machine: {item.MachineName}, SumBill: {item.SumBill}");
                }

                return reportData;
            }
        }



        // Phương thức để cập nhật một phiên và lấy báo cáo ngay sau đó
        public void UpdateSessionAndGenerateReport(int sessionId, decimal updatedTotalAmount)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Lấy session từ cơ sở dữ liệu
                    var session = context.MachineSessions.Find(sessionId);

                    if (session != null)
                    {
                        // Cập nhật số tiền tổng cho phiên
                        session.TotalAmount = updatedTotalAmount;
                        context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
                    }

                    // Cam kết giao dịch
                    transaction.Commit();

                    // Gọi phương thức lấy báo cáo sau khi cập nhật
                    var report = GetRevenueReport();

                    // In ra console (hoặc xử lý hiển thị theo ý bạn)
                    foreach (var item in report)
                    {
                        Console.WriteLine($"Session: {item.MachineName}, Total Revenue: {item.SumBill}");
                    }
                }
                catch (Exception)
                {
                    // Khôi phục giao dịch nếu có lỗi
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }    
}