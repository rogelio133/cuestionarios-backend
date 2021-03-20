using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class User
    {
        public int IDUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime DateLastLogin { get; set; }
        public int IDStatus { get; set; }
        public string UserName { get; set; }
        public DateTime DateLastPasswordChange { get; set; }
    }
}
