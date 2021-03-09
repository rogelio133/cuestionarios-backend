using System;
using System.Collections.Generic;

namespace Entities
{
    public class Questionnaire
    {
        public int IDQuestionnaire { get; set; }
        public int IDUser { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int NoQuestions { get; set; }

        public List<Question> Questions { get; set; }

            //generar un uuid como llave
    }
}
