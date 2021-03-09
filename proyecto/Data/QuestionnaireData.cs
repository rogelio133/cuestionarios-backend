using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data
{
    public static class QuestionnaireData
    {
        public static List<Questionnaire> GetQuestionnaires(int iDUser)
        {
            var query = "[dbo].[usp_QuestionnairesByUser_GET]";
            List<Questionnaire> questionnaires = new List<Questionnaire>();
            try
            {

                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = Helper.GetConnection();
                    connection.Open();
                    var command = new SqlCommand(query)
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (command.Connection)
                    {
                        command.Parameters.Add("@piDUser", SqlDbType.VarChar).Value = iDUser;

                        var reader = command.ExecuteReader();
                        questionnaires = Converter<Questionnaire>.ConvertDataSetToList(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return questionnaires;
        }
        public static void Save(Questionnaire item, DataTable dtQuestions, DataTable dtOptions)
        {
            var query = "[dbo].[usp_SaveQuestionnaire_INS]";
            try
            {

                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = Helper.GetConnection();
                    connection.Open();
                    var command = new SqlCommand(query)
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (command.Connection)
                    {
                        command.Parameters.Add("@pIDUser", SqlDbType.Int).Value = item.IDUser;
                        command.Parameters.Add("@pName", SqlDbType.VarChar).Value = item.Name;
                        command.Parameters.Add("@pCode", SqlDbType.VarChar).Value = item.Code;
                        command.Parameters.Add("@pNoQuestions", SqlDbType.Int).Value = item.NoQuestions;
                        command.Parameters.Add("@pDTQuestions", SqlDbType.Structured).Value = dtQuestions;
                        command.Parameters.Add("@pDTOptions", SqlDbType.Structured).Value = dtOptions;

                        item.IDQuestionnaire = Convert.ToInt32( command.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        public static List<Option> GetOptionsOfQuestions(DataTable dtQuestions)
        {
            var query = "[dbo].[usp_OptionsOfQuestions_GET]";
            List<Option> options = new List<Option>();
            try
            {

                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = Helper.GetConnection();
                    connection.Open();
                    var command = new SqlCommand(query)
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (command.Connection)
                    {
                        command.Parameters.Add("@pdtQuestions", SqlDbType.Structured).Value = dtQuestions;

                        var reader = command.ExecuteReader();
                        options = Converter<Option>.ConvertDataSetToList(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return options;
        }
        public static List<Question> GetQuestionsOfQuestionnaires(DataTable dtQuestionnaires)
        {
            var query = "[dbo].[usp_QuestionsOfQuestionnaires_GET]";
            List<Question> questions = new List<Question>();
            try
            {

                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = Helper.GetConnection();
                    connection.Open();
                    var command = new SqlCommand(query)
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (command.Connection)
                    {
                        command.Parameters.Add("@pDTQuestionnaires", SqlDbType.Structured).Value = dtQuestionnaires;

                        var reader = command.ExecuteReader();
                        questions = Converter<Question>.ConvertDataSetToList(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return questions;
        }
    }
}
