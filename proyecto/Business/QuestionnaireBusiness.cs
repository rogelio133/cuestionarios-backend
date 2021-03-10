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
        public static questionnaire getQuestionnaire(string token,string code)
        {
            int IDUser = UserBusiness.GetIDUser(token);
            Questionnaire questionnaire = getQuestionnaireByCode(code);

          

            //validar que la pregunta pertenezca al usuario en cuestion

            questionnaire respuesta = new questionnaire
            {
                Name = questionnaire.Name,
                Code = questionnaire.Code,
                Questions = new List<question>()
            };

            questionnaire.Questions.ForEach(q => {
                question question = new question
                {
                    IDQuestion = q.IDQuestion,
                    Name = q.Name,
                    Options = new List<option>()
                };

                q.Options.ForEach(o => {
                    question.Options.Add(new option {
                        IDOption = o.IDOption,Name= o.Name, Correct = o.Correct
                    });
                });
                respuesta.Questions.Add(question);
            });

           return respuesta;
        }

        public static Questionnaire getQuestionnaireByCode( string code)
        {
            Questionnaire questionnaire = QuestionnaireData.GetQuestionnaire(code);

            List<Question> questions = QuestionnaireData.GetQuestionsOfQuestionnaire(questionnaire.IDQuestionnaire);
            List<Option> options = new List<Option>();

            #region get options

            DataTable dtQuestions = GetTable_Question();

            questions.ForEach(q => {
                dtQuestions.Rows.Add(q.IDQuestion, "");
            });

            options = QuestionnaireData.GetOptionsOfQuestions(dtQuestions);

            questions.ForEach(question => {
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
                NoQuestions = questionnaire.NoQuestions
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

            item.Questions.ForEach(q => {
                dtQuestions.Rows.Add(0,q.Name);
                q.Options.ForEach(o => {
                    dtOptions.Rows.Add(q.Name,o.Name,o.Correct);
                });
            });

            QuestionnaireData.Save(item, dtQuestions, dtOptions);
        }


        public static questionnaire ValidateCode(string code)
        {
            Questionnaire questionnaire = getQuestionnaireByCode(code);
            questionnaire respuesta = null;

            if(questionnaire != null)
            {
                respuesta = new questionnaire
                {
                     Name = questionnaire.Name,
                    Questions = new List<question>()
                };

                questionnaire.Questions.ForEach(q => {
                    question question = new question
                    {
                        IDQuestion = q.IDQuestion,
                        Name = q.Name,
                        Options = new List<option>()
                    };

                    q.Options.ForEach(o => {
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

        private static DataTable GetTable_Questionnaire()
        {
            var Table_Questionnaire = new DataTable();

            Table_Questionnaire.Columns.Add("IDQuestionnaire", typeof(int));

            return Table_Questionnaire;
        }

    }
}
