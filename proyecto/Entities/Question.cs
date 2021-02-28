using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Question
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Option> Options { get; set; }
    }
}
