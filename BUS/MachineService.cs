using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MachineService
    {
        private readonly CyBerModel context; // Lớp truy cập dữ liệu

        public MachineService()
        {
            context = new CyBerModel();
        }

        public List<Machine> GetMachinesByZone(int zoneId)
        {
            return context.Machines.Where(m => m.ZoneId == zoneId).ToList(); // Lấy máy theo zone
        }

        public void TurnOnMachine(int machineId)
        {
            var machine = context.Machines.Find(machineId);
            if (machine != null)
            {
                machine.Status = "Online"; // Cập nhật trạng thái máy
                context.SaveChanges(); // Lưu thay đổi
            }
        }
        public void EndSession(int sessionId, DateTime endTime)
        {
            // Giả sử bạn có DbContext được cấu hình sẵn
            using (var context = new CyBerModel())
            {
                var session = context.MachineSessions.Find(sessionId);
                if (session != null)
                {
                    session.EndTime = endTime;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Không tìm thấy phiên giao dịch.");
                }
            }
        }

        public void TurnOffMachine(int machineId)
        {
            var machine = context.Machines.Find(machineId);
            if (machine != null)
            {
                machine.Status = "Offline"; // Cập nhật trạng thái máy
                context.SaveChanges(); // Lưu thay đổi
            }
        }
        public MachineSession GetSessionBy(int sessionId)
        {
            return context.MachineSessions
                           .FirstOrDefault(session => session.SessionId == sessionId);
        }

        public void UpdateTotalAmount(int sessionId, decimal totalAmount)
        {
            // Thực hiện cập nhật tổng tiền cho phiên hoạt động trong cơ sở dữ liệu
            using (var context = new CyBerModel())
            {
                var session = context.MachineSessions.Find(sessionId);
                if (session != null)
                {
                    session.TotalAmount = totalAmount;
                    context.SaveChanges();
                }
            }
        }
        public int GetCurrentSessionIdForMachine(int machineId)
        {
            // Lấy sessionId cho máy đang hoạt động
            return context.MachineSessions
                .Where(ms => ms.MachineId == machineId && ms.EndTime == null) // Kiểm tra xem máy có phiên hoạt động không
                .Select(ms => ms.SessionId)
                .FirstOrDefault();
        }
        public void StartNewSession(MachineSession session)
        {
            using (var context = new CyBerModel())
            {
                context.MachineSessions.Add(session);
                context.SaveChanges();
            }
        }
        public void UpdateMachineStatus(int machineId, string status)
        {
            var machine =context.Machines.Find(machineId);
            if (machine != null)
            {
                machine.Status = status; // Cập nhật trạng thái
                context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            else
            {
                throw new Exception("Máy không tồn tại.");
            }
        }



        ////// REPORT //////
       
    }
}