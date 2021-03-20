using Data;
using Entities;
using Entities.custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace Business
{
    public static class UserBusiness
    {
        public static string CreateAccount(string username, string name ,string password)
        {

            User user = new User {
                Name = name,
                UserName = username,
                IDStatus = (int)Enums.UserStatus.Enabled,
                Password = ComputeSha256Hash(password)
            };

            UserData.CreateAccount(user);

            string token = GenerateToken(user.IDUser);
            return token;
        }
        private static string GenerateToken(int IDUser)
        {
            string token = Guid.NewGuid().ToString();

            UserData.SaveToken(IDUser, token);

            return token;
        }
        public static int GetIDUser(string token)
        {
            return UserData.GetIDUser(token);
        }

        public static bool IsValidToken(string token)
        {
            User user = UserData.ValidateToken(token);

            //validar estatus y fecha ultimo login!

            return user != null && user.IDStatus == (int)Enums.UserStatus.Enabled;
        }

        public static bool IsValidUser(string username)
        {
            return UserData.IsValidUser(username);
        } 

        public static User ValidateLogin(string username, string password)
        {
            password = ComputeSha256Hash(password);
            User user = UserData.ValidateLogin(username, password);

            if (user != null && user.IDStatus == (int)Enums.UserStatus.Enabled)
            {
                user.Token = GenerateToken(user.IDUser);
            }


            return user;
        }


        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
