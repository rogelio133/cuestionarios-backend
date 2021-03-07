using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class QuestionnaireAnswer
    {
        public string Name { get; set; }
        public List<QuestionnaireAnswersDetail> Answers { get; set; }
    }
}
