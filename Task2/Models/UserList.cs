using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class UserList
    {

        public string ID { get; set; }
        public string Email { get; set; }
        public DateTime Auth { get; set; }
        public DateTime Register { get; set; }
        public String Role { get; set; }
        public bool Status { get; set; }
        
    }
}
