using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Option
    {
        public int IDOption { get; set; }
        public int IDQuestion { get; set; }
        public string Name { get; set; }
        public bool Correct { get; set; }
    }
}
