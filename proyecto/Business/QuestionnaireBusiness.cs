using Data;
using Entities;
using Entities.response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business
{
    public class QuestionnaireBusiness
    {
        public static questionnaire getQuestionnaire(string token, string code)
        {
            int IDUser = UserBusiness.GetIDUser(token);
            Questionnaire _questionnaire = getQuestionnaireByCode(code);

            if (_questionnaire == null)
            {
                string error = string.Format("Questionnaire '{0}' doesn't exists. IDuser : {1}", code, IDUser);
                throw new ApplicationException("El cuestionario solicitado no esta disponible");
            }

            if (_questionnaire.IDUser != IDUser)
            {
                string error = string.Format("Questionnaire '{0}' isn't valid for IDuser : {1}", code, IDUser);
                throw new ApplicationException("El cuestionario solicitado no esta disponible");
            }

            _questionnaire.answers = GetAnswers(_questionnaire.IDQuestionnaire);

            questionnaire respuesta = new questionnaire
            {
                Name = _questionnaire.Name,
                Code = _questionnaire.Code,
                Questions = new List<question>()
            };

            _questionnaire.Questions.ForEach(q =>
            {
                question question = new question
                {
                    IDQuestion = q.IDQuestion,
                    Name = q.Name,
                    Options = new List<option>()
                };

                q.Options.ForEach(o =>
                {
                    question.Options.Add(new option
                    {
                        IDOption = o.IDOption,
                        Name = o.Name,
                        Correct = o.Correct
                    });
                });
                respuesta.Questions.Add(question);
            });

            respuesta.Exams = _questionnaire.answers.Select((a, index) => new answers
            {
                ID = a.IDAnswer,
                Name = a.Name,
                AnswersCorrect = a.AnswersCorrect,
                AnswersFailed = a.AnswersFailed,
                Score = a.Score,
                Date = a.sDate,
                Time = a.sTime
            }).ToList();

            return respuesta;
        }

        public static Questionnaire getQuestionnaireByCode(string code)
        {
            int? IDQuestionnaire = QuestionnaireData.GetIDQuestionnaireByCode(code);
            Questionnaire questionnaire = null; 

            if (IDQuestionnaire != null)
            {
                questionnaire = getQuestionnaireByID(IDQuestionnaire.Value);
            }

            return questionnaire;
        }

        private static Questionnaire getQuestionnaireByID(int IDQuestionnaire)
        {
            Questionnaire questionnaire = QuestionnaireData.GetQuestionnaire(IDQuestionnaire);
            List<Question> questions = QuestionnaireData.GetQuestionsOfQuestionnaire(questionnaire.IDQuestionnaire);
            List<Option> options = new List<Option>();

            #region get options

            DataTable dtQuestions = GetTable_Question();

            questions.ForEach(q =>
            {
                dtQuestions.Rows.Add(q.IDQuestion, "");
            });

            options = QuestionnaireData.GetOptionsOfQuestions(dtQuestions);

            questions.ForEach(question =>
            {
                question.Options = options.Where(o => o.IDQuestion == question.IDQuestion).ToList();
            });

            questionnaire.Questions = questions;

            #endregion

            return questionnaire;
        }

        public static List<questionnaire> getQuestionnaires(string token)
        {
            int IDUser = UserBusiness.GetIDUser(token);

            List<Questionnaire> questionnaires = QuestionnaireData.GetQuestionnaires(IDUser);

            List<questionnaire> questionnairesResponse = questionnaires.Select(questionnaire => new questionnaire
            {
                Name = questionnaire.Name,
                Code = questionnaire.Code,
                NoQuestions = questionnaire.NoQuestions,
                Answers = questionnaire.Answers
            }).ToList();

            return questionnairesResponse;
        }

        public static void Save(Questionnaire item, string token)
        {
            item.IDUser = UserBusiness.GetIDUser(token);
            item.Code = CreateRandom(6);
            item.NoQuestions = item.Questions.Count;

            DataTable dtQuestions = GetTable_Question();
            DataTable dtOptions = GetTable_Option();

            item.Questions.ForEach(q =>
            {
                dtQuestions.Rows.Add(0, q.Name);
                q.Options.ForEach(o =>
                {
                    dtOptions.Rows.Add(q.Name, o.Name, o.Correct);
                });
            });

            QuestionnaireData.Save(item, dtQuestions, dtOptions);
        }

        public static object SaveQuestionnaireAnswer(string code, QuestionnaireAnswer exam)
        {
            Questionnaire questionnaire = getQuestionnaireByCode(code);

            int answersCorrect = 0;

            List<question> userQuestions = GetDetailAnswer(questionnaire.Questions,exam.Answers);

          
            questionnaire.Questions.ForEach(q =>
            {
                QuestionnaireAnswersDetail questionAnswer = exam.Answers.First(a => a.IDQuestion == q.IDQuestion);
                bool isCorrect = q.Options.First(o => o.Correct).IDOption == questionAnswer.IDOptionSelected;

                if (isCorrect)
                {
                    answersCorrect++;
                }
               
            });

            exam.IDQuestionnaire = questionnaire.IDQuestionnaire;
            exam.AnswersCorrect = answersCorrect;
            exam.AnswersFailed = questionnaire.Questions.Count - answersCorrect;
            exam.Score = (100 * answersCorrect) / questionnaire.Questions.Count;

            DataTable tdDetail = GetTable_QuestionnaireAnswersDetail();

            exam.Answers.ForEach(a =>
            {
                tdDetail.Rows.Add(a.IDQuestion, a.IDOptionSelected);
            });

            QuestionnaireData.SaveQuestionnaireAnswer(exam, tdDetail);

            List<AnswerStats> stats = new List<AnswerStats> {
                new AnswerStats {ID=1, Description="# ACIERTOS",Value=exam.AnswersCorrect.ToString()},
                new AnswerStats {ID=2,Description="# FALLAS",Value=exam.AnswersFailed.ToString()},
                new AnswerStats {ID=3,Description="CALIFICACIÓN",Value=exam.Score.ToString()},
                new AnswerStats {ID=4,Description="TIEMPO",Value= string.Format("{0} min", (int)(exam.Time/60000)  )},
            };

            return new
            {
                stats,
                userQuestions
            };

        }

        public static questionnaire ValidateCode(string code)
        {
            Questionnaire questionnaire = getQuestionnaireByCode(code);
            questionnaire respuesta = null;

            if (questionnaire != null)
            {
                respuesta = new questionnaire
                {
                    Name = questionnaire.Name,
                    Code = questionnaire.Code,
                    Questions = new List<question>()
                };

                questionnaire.Questions.ForEach(q =>
                {
                    question question = new question
                    {
                        IDQuestion = q.IDQuestion,
                        Name = q.Name,
                        Options = new List<option>()
                    };

                    q.Options.ForEach(o =>
                    {
                        question.Options.Add(new option
                        {
                            IDOption = o.IDOption,
                            Name = o.Name
                        });
                    });
                    respuesta.Questions.Add(question);
                });
            }


            return respuesta;
        }






        private static string CreateRandom(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private static List<QuestionnaireAnswer> GetAnswers(int IDQuestionnaire)
        {
            List<QuestionnaireAnswer> answers = QuestionnaireData.GetAnswers(IDQuestionnaire);
            return answers;
        }

        private static QuestionnaireAnswer GetAnswer(int IDAnswer)
        {
            QuestionnaireAnswer exam = QuestionnaireData.GetAnswer(IDAnswer);

            exam.Answers = QuestionnaireData.GetQuestionnaireAnswerDetails(IDAnswer);

            return exam;
        }
        public  static List<question> GetDetailAnswer(int IDAnswer)
        {
            QuestionnaireAnswer exam = GetAnswer(IDAnswer);

            if (exam == null)
            {
                throw new ApplicationException("La información solicitada no esta disponible");
            }

            Questionnaire questionnaire = getQuestionnaireByID(exam.IDQuestionnaire);

            return GetDetailAnswer(questionnaire.Questions, exam.Answers);
        }
        private static List<question> GetDetailAnswer(List<Question> questions, List<QuestionnaireAnswersDetail> Answers)
        {
            List<question> examQuestions = new List<question>();

            int x = 1;
            questions.ForEach(q =>
            {
                QuestionnaireAnswersDetail questionAnswer = Answers.First(a => a.IDQuestion == q.IDQuestion);
                bool isCorrect = q.Options.First(o => o.Correct).IDOption == questionAnswer.IDOptionSelected;

                List<option> options = new List<option>();
                int y = 1;
                q.Options.ForEach(o =>
                {
                    options.Add(new option
                    {
                        IDOption = y,
                        Name = o.Name,
                        Selected = questionAnswer.IDOptionSelected == o.IDOption,
                        Correct = o.Correct
                    });
                    y++;
                });

                examQuestions.Add(new question { IDQuestion = x, Name = q.Name, Correct = isCorrect, Options = options });

                x++;
            });

            return examQuestions;
        }

        private static DataTable GetTable_Option()
        {
            var Table_Option = new DataTable();

            Table_Option.Columns.Add("Question", typeof(string));
            Table_Option.Columns.Add("Name", typeof(string));
            Table_Option.Columns.Add("Correct", typeof(bool));

            return Table_Option;
        }

        private static DataTable GetTable_Question()
        {
            var Table_Question = new DataTable();

            Table_Question.Columns.Add("IDQuestion", typeof(int));
            Table_Question.Columns.Add("Name", typeof(string));

            return Table_Question;
        }

        private static DataTable GetTable_QuestionnaireAnswersDetail()
        {
            var Table_QuestionnaireAnswersDetail = new DataTable();

            Table_QuestionnaireAnswersDetail.Columns.Add("IDQuestion", typeof(int));
            Table_QuestionnaireAnswersDetail.Columns.Add("IDOptionSelected", typeof(int));


            return Table_QuestionnaireAnswersDetail;
        }

    }
}
