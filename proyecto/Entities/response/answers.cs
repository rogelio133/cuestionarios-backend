using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.response
{
    public class answers
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AnswersCorrect { get; set; }
        public int AnswersFailed { get; set; }
        public int Score { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
