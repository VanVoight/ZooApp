using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.Models
{
    public class Animal
    {
        [Key] public int Id_Animal { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Habitat { get; set; }
        public string Description  { get; set; }
    }
}
