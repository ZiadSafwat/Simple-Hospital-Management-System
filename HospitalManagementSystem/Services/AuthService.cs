using System.Linq;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class AuthService
    {
        public User Login(string username, string password)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Users
                    .FirstOrDefault(u => u.Username == username
                                      && u.Password == password
                                      && u.IsActive);
            }
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var context = new HospitalDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == oldPassword);
                if (user != null)
                {
                    user.Password = newPassword;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
