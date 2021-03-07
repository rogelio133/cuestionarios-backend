using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class QuestionnaireAnswersDetail
    {
        public int ID { get; set; }
        public int IDQuestion { get; set; }
        public int IDOptionSelected { get; set; }
    }
}
