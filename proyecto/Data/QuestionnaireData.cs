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
        public static Questionnaire GetQuestionnaire(string code)
        {
            var query = "[dbo].[usp_QuestionnaireByCode_GET]";
            Questionnaire questionnaire = null;
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
                        command.Parameters.Add("@pCode", SqlDbType.VarChar).Value = code;

                        var reader = command.ExecuteReader();
                        questionnaire = Converter<Questionnaire>.ConvertDataSetToList(reader).FirstOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return questionnaire;
        }

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
                        command.Parameters.Add("@piDUser", SqlDbType.Int).Value = iDUser;

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
        
        public static List<QuestionnaireAnswer> GetAnswers(int iDQuestionnaire)
        {
            var query = "[dbo].[usp_QuestionnaireAnswers_GET]";
            List<QuestionnaireAnswer> answers = new List<QuestionnaireAnswer>();
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
                        command.Parameters.Add("@pIDQuestionnaire", SqlDbType.Int).Value = iDQuestionnaire;

                        var reader = command.ExecuteReader();
                        answers = Converter<QuestionnaireAnswer>.ConvertDataSetToList(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return answers;
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

        public static List<Question> GetQuestionsOfQuestionnaire(int IDQuestionnaire)
        {
            var query = "[dbo].[usp_QuestionsInQuestionnaire_GET]";
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
                        command.Parameters.Add("@pIDQuestionnaire", SqlDbType.Int).Value = IDQuestionnaire;

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

                        item.IDQuestionnaire = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void SaveQuestionnaireAnswer(QuestionnaireAnswer exam, DataTable tdDetail)
        {
            var query = "[dbo].[usp_SaveAnswer_INS]";
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
                        command.Parameters.Add("@pName", SqlDbType.VarChar).Value = exam.Name;
                        command.Parameters.Add("@pIDQuestionnaire", SqlDbType.Int).Value = exam.IDQuestionnaire;
                        command.Parameters.Add("@pAnswersCorrect", SqlDbType.Int).Value = exam.AnswersCorrect;
                        command.Parameters.Add("@pAnswersFailed", SqlDbType.Int).Value = exam.AnswersFailed;
                        command.Parameters.Add("@pScore", SqlDbType.Int).Value = exam.Score;
                        command.Parameters.Add("@pTime", SqlDbType.BigInt).Value = exam.Time;

                        command.Parameters.Add("@pDTDetail", SqlDbType.Structured).Value = tdDetail;

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
