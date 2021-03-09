using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Question
    {
        public int IDQuestion { get; set; }
        public int IDQuestionnaire { get; set; }
        public string Name { get; set; }
        public List<Option> Options { get; set; }
    }
}
