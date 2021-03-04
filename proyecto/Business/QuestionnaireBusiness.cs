using Entities;
using Entities.response;
using System;
using System.Collections.Generic;
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
                ID = 666,
                Name = "Mini Test React",
                Code = "133515",
                Questions = new List<Question>
                {
                    new Question {ID=1,Name="Pregunta 1",Options= new List<Option> { new Option { ID=1,Name="Opcion 1", Correct= false },new Option { ID=2,Name="Opcion 2", Correct= false },new Option { ID=3,Name="Opcion 3", Correct= true },  }},
                    new Question {ID=2,Name="Pregunta 2",Options= new List<Option> { new Option { ID=1,Name="Opcion a", Correct= false },new Option { ID=2,Name="Opcion b", Correct= false },new Option { ID=3,Name="Opcion c", Correct= true },  }}
                }
            };

            //validar que la pregunta pertenezca al usuario en cuestion

            questionnaire respuesta = new questionnaire
            {
                ID = item.ID,
                Name = item.Name,
                Code = item.Code,
                Questions = new List<question>()
            };

            item.Questions.ForEach(q => {
                question question = new question
                {
                    ID = q.ID,
                    Name = q.Name,
                    Options = new List<option>()
                };

                q.Options.ForEach(o => {
                    question.Options.Add(new option { 
                        ID = o.ID,Name= o.Name, Correct = o.Correct
                    });
                });
                respuesta.Questions.Add(question);
            });

           return respuesta;
        }

        public static List<questionnaire> getQuestionnaires()
        {
            Questionnaire item = new Questionnaire
            {
                ID = 666,
                Name = "Mini Test React",
                Code = "133515",
                NoQuestions = 2,
                Questions = new List<Question>
                {
                    new Question {ID=1,Name="Pregunta 1",Options= new List<Option> { new Option { ID=1,Name="Opcion 1", Correct= false },new Option { ID=2,Name="Opcion 2", Correct= false },new Option { ID=3,Name="Opcion 3", Correct= true },  }},
                    new Question {ID=2,Name="Pregunta 2",Options= new List<Option> { new Option { ID=1,Name="Opcion a", Correct= false },new Option { ID=2,Name="Opcion b", Correct= false },new Option { ID=3,Name="Opcion c", Correct= true },  }}
                }
            };

            //validar que la pregunta pertenezca al usuario en cuestion

            questionnaire respuesta = new questionnaire
            {
                ID = item.ID,
                Name = item.Name,
                Code = item.Code,
                NoQuestions = item.NoQuestions,
                Questions = new List<question>()
            };

            item.Questions.ForEach(q => {
                question question = new question
                {
                    ID = q.ID,
                    Name = q.Name,
                    Options = new List<option>()
                };

                q.Options.ForEach(o => {
                    question.Options.Add(new option
                    {
                        ID = o.ID,
                        Name = o.Name,
                        Correct = o.Correct
                    });
                });
                respuesta.Questions.Add(question);
            });


            List<questionnaire> lista = new List<questionnaire> 
            {
                respuesta,respuesta
            };

            return lista;
        }

        public static questionnaire ValidateCode(string code)
        { 
             
            questionnaire respuesta = null;


            if (code == "111111")
            {
                Questionnaire item = new Questionnaire
                {
                    ID = 666,
                    Name = "Mini Test React",
                    Code = "133515",
                    Questions = new List<Question>
                {
                    new Question {ID=1,Name="Pregunta 1",Options= new List<Option> { new Option { ID=1,Name="Opcion 1", Correct= false },new Option { ID=2,Name="Opcion 2", Correct= false },new Option { ID=3,Name="Opcion 3", Correct= true },  }},
                    new Question {ID=2,Name="Pregunta 2",Options= new List<Option> { new Option { ID=1,Name="Opcion a", Correct= false },new Option { ID=2,Name="Opcion b", Correct= false },new Option { ID=3,Name="Opcion c", Correct= true },  }}
                }
                };

                respuesta = new questionnaire
                {
                    ID = item.ID,
                    Name = item.Name,
                    Questions = new List<question>()
                };

                item.Questions.ForEach(q => {
                    question question = new question
                    {
                        ID = q.ID,
                        Name = q.Name,
                        Options = new List<option>()
                    };

                    q.Options.ForEach(o => {
                        question.Options.Add(new option
                        {
                            ID = o.ID,
                            Name = o.Name
                        });
                    });
                    respuesta.Questions.Add(question);
                });
            }


            return respuesta;
        }
    }
}
