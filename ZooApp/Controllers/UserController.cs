using Microsoft.EntityFrameworkCore;
using System.Linq;
using ZooApp.Data;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class UserController
    {
        private readonly ZooContext _context;

        public UserController(ZooContext context)
        {
            _context = context;
        }

        public User AuthenticateUser(string username, string password, string accountType)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.Typ_konta == accountType);
        }

        public bool RegisterUser(string username, string password)
        {
            if (_context.Users.FirstOrDefault(u => u.Username == username) == null)
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    Typ_konta = "standard"
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool ChangeAccountType(string username, string newAccountType)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Username == username);

            if (userToUpdate != null)
            {
                userToUpdate.Typ_konta = newAccountType;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}