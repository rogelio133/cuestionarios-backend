using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.response
{
    public class question
    {
        public int IDQuestion { get; set; }
        public string Name { get; set; }
        public List<option> Options { get; set; }

    }
}
