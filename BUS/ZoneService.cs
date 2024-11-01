using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ZoneService
    {
        private CyBerModel context; // Lớp truy cập dữ liệu

        public ZoneService()
        {
            context = new CyBerModel();
        }

        public List<Zone> GetAllZones()
        {
            return context.Zones.ToList(); // Lấy tất cả các zone từ cơ sở dữ liệu
        }
    }
}
