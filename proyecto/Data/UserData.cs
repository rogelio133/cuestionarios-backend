using Entities;
using Entities.response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data
{
    public static class UserData
    {
        public static void CreateAccount(User user)
        {
            var query = "[dbo].[usp_CreateAccount_INS]";
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
                        command.Parameters.Add("@pName", SqlDbType.VarChar).Value = user.Name;
                        command.Parameters.Add("@pPassword", SqlDbType.VarChar).Value = user.Password;
                        command.Parameters.Add("@pUsername", SqlDbType.VarChar).Value = user.UserName;
                        command.Parameters.Add("@pIDEstatus", SqlDbType.VarChar).Value = user.IDStatus;

                        user.IDUser = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetIDUser(string token)

        {
            var query = "[dbo].[usp_IDUserByToken_GET]";
            int IDUser = 0;
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
                        command.Parameters.Add("@pToken", SqlDbType.VarChar).Value = token;

                        IDUser = Convert.ToInt32( command.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IDUser;
        }

        public static bool IsValidUser(string username)
        {
            var query = "[dbo].[usp_ValidateUsernameExists_GET]";
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
                        command.Parameters.Add("@pUsername", SqlDbType.VarChar).Value = username;

                        return  Convert.ToInt32(command.ExecuteScalar())>0;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveToken(int IDUser, string token)

        {
            var query = "[dbo].[usp_saveUserToken_UPT]";
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
                        command.Parameters.Add("@pIDUser", SqlDbType.Int).Value = IDUser;
                        command.Parameters.Add("@pToken", SqlDbType.VarChar).Value = token;

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User ValidateLogin(string username, string password)

        {
            var query = "[dbo].[usp_validateLogin_GET]";
            User user = null;
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
                        command.Parameters.Add("@pUserName", SqlDbType.VarChar).Value = username ;
                        command.Parameters.Add("@pPassword", SqlDbType.VarChar).Value = password;

                        var reader = command.ExecuteReader();
                        user = Converter<User>.ConvertDataSetToList(reader).FirstOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }

        public static User ValidateToken(string token)
        {
            var query = "[dbo].[usp_validateToken_GET]";
            User user = null;
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
                        command.Parameters.Add("@pToken", SqlDbType.VarChar).Value = token;

                        var reader = command.ExecuteReader();
                        user = Converter<User>.ConvertDataSetToList(reader).FirstOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }


        

    }
}
