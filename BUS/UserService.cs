using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    
    public class UserService
    {
        private CyBerModel context;
        public UserService()
        {
            context = new CyBerModel();
        }

        public User Login(string username, string passwordHash)
        {
            return context.Users
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash && u.IsActive);
        }
        public string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Admin";
                case 2:
                    return "Staff";
                case 3:
                    return "Customer";
                default:
                    return "Unknown"; // Hoặc có thể là một thông báo khác
            }
        }
    }
}
