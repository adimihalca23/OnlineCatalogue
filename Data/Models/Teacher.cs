using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public Address Address { get; set; }
        public Subject Subject { get; set; }
    }
}
