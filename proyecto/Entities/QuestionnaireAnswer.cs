using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class QuestionnaireAnswer
    {
        
        public int IDAnswer { get; set; }
        public int IDQuestionnaire { get; set; }
        public string Name { get; set; }
        public int AnswersCorrect { get; set; }
        public int AnswersFailed { get; set; }
        public int Score { get; set; }
        public double Time { get; set; }
        public DateTime CreationDate { get; set; }
        public List<QuestionnaireAnswersDetail> Answers { get; set; }

        public string sDate { get { return CreationDate.ToString("dd/MM/yyyy HH:mm"); } }
        public string sTime { get { return string.Format("{0} min", (int)(Time/60000)  ); } }
    }
}
