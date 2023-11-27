using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.Models
{
    public class User
    {
        [Key] public int Id_User { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Typ_konta { get; set; }

    }
}
