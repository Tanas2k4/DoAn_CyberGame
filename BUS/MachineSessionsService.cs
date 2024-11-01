using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MachineSessionsService
    {
        private readonly CyBerModel context;
        private readonly MachineSessionsService machineSessionsService;

        public MachineSessionsService()
        {
            context = new CyBerModel(); // Khởi tạo DbContext
            machineSessionsService = new MachineSessionsService(); // Khởi tạo MachineSessionsService
        }

        public List<MachineSession> GetMachineSessions()
        {
            return context.MachineSessions.AsNoTracking().ToList();
        }
    }
}
