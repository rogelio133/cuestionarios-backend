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
        public static questionnaire getQuestionnaire(int ID)
        {
            Questionnaire item = new Questionnaire
            {
                IDQuestionnaire = 666,
                Name = "Mini Test React",
                Code = "133515",
                Questions = new List<Question>
                {
                    new Question {IDQuestion=1,Name="Pregunta 1",Options= new List<Option> { new Option { IDQuestion=1,Name="Opcion 1", Correct= false },new Option { IDQuestion=2,Name="Opcion 2", Correct= false },new Option { IDQuestion=3,Name="Opcion 3", Correct= true },  }},
                    new Question {IDQuestion=2,Name="Pregunta 2",Options= new List<Option> { new Option { IDQuestion=1,Name="Opcion a", Correct= false },new Option { IDQuestion=2,Name="Opcion b", Correct= false },new Option { IDQuestion=3,Name="Opcion c", Correct= true },  }}
                }
            };

            //validar que la pregunta pertenezca al usuario en cuestion

            questionnaire respuesta = new questionnaire
            {
                Name = item.Name,
                Code = item.Code,
                Questions = new List<question>()
            };

            item.Questions.ForEach(q => {
                question question = new question
                {
                    ID = q.IDQuestion,
                    Name = q.Name,
                    Options = new List<option>()
                };

                q.Options.ForEach(o => {
                    question.Options.Add(new option { 
                        ID = o.IDOption,Name= o.Name, Correct = o.Correct
                    });
                });
                respuesta.Questions.Add(question);
            });

           return respuesta;
        }

        public static List<questionnaire> getQuestionnaires(string token)
        {
            int IDUser = UserBusiness.GetIDUser(token);

            List<Questionnaire> questionnaires = QuestionnaireData.GetQuestionnaires(IDUser);
            
            /*
            List<Question> questions = new List<Question>();
            List<Option> options = new List<Option>();

            #region get questions

            DataTable dtQuestionnaires = GetTable_Question();

            questionnaires.ForEach(q => {
                dtQuestionnaires.Rows.Add(q.IDQuestionnaire);
            });

            questions = QuestionnaireData.GetQuestionsOfQuestionnaires(dtQuestionnaires);

            questionnaires.ForEach(q => {
                q.Questions = questions.Where(question => question.IDQuestionnaire == q.IDQuestionnaire).ToList();
            });

            #endregion


            #region get options

            DataTable dtQuestions = GetTable_Question();

            questions.ForEach(q => {
                dtQuestions.Rows.Add(q.IDQuestion, "");
            });

            options = QuestionnaireData.GetOptionsOfQuestions(dtQuestions);

            questions.ForEach(question => {
                question.Options = options.Where(o => o.IDQuestion == question.IDQuestion).ToList();
            });

            #endregion

            Questionnaire item = new Questionnaire
            {
                IDQuestionnaire = 666,
                Name = "Mini Test React",
                Code = "133515",
                NoQuestions = 2,
                Questions = new List<Question>
                {
                    new Question {IDQuestion=1,Name="Pregunta 1",Options= new List<Option> { new Option { IDOption=1,Name="Opcion 1", Correct= false },new Option { IDOption=2,Name="Opcion 2", Correct= false },new Option { IDOption=3,Name="Opcion 3", Correct= true },  }},
                    new Question {IDQuestion=2,Name="Pregunta 2",Options= new List<Option> { new Option { IDOption=1,Name="Opcion a", Correct= false },new Option { IDOption=2,Name="Opcion b", Correct= false },new Option { IDOption=3,Name="Opcion c", Correct= true },  }}
                }
            };

           */
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
             
            questionnaire respuesta = null;


            if (code == "111111")
            {
                Questionnaire item = new Questionnaire
                {
                    IDQuestionnaire = 666,
                    Name = "Mini Test React",
                    Code = "133515",
                    Questions = new List<Question>
                {
                    new Question {IDQuestion=1,Name="Pregunta 1",Options= new List<Option> { new Option { IDOption=10,Name="Opcion 1", Correct= false },new Option { IDOption=11,Name="Opcion 2", Correct= false },new Option { IDOption=13,Name="Opcion 3", Correct= true },  }},
                    new Question {IDQuestion=2,Name="Pregunta 2",Options= new List<Option> { new Option { IDOption=20,Name="Opcion a", Correct= false },new Option { IDOption=21,Name="Opcion b", Correct= false },new Option { IDOption=23,Name="Opcion c", Correct= true },  }},
                    new Question {IDQuestion=3,Name="Pregunta 3",Options= new List<Option> { new Option { IDOption=30,Name="Opcion x", Correct= false },new Option { IDOption=31,Name="Opcion y", Correct= false },new Option { IDOption=33,Name="Opcion z", Correct= true },  }}

                }
                };

                respuesta = new questionnaire
                {
                     Name = item.Name,
                    Questions = new List<question>()
                };

                item.Questions.ForEach(q => {
                    question question = new question
                    {
                        ID = q.IDQuestion,
                        Name = q.Name,
                        Options = new List<option>()
                    };

                    q.Options.ForEach(o => {
                        question.Options.Add(new option
                        {
                            ID = o.IDOption,
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
