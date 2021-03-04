using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.response
{
    public class questionnaire
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int NoQuestions { get; set; }
        public List<question> Questions { get; set; }
    }
}
