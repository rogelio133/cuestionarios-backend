using Data;
using Entities;
using Entities.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public static class UserBusiness
    {
        private static string GenerateToken(int IDUser)
        {
            string token = Guid.NewGuid().ToString();

            UserData.SaveToken(IDUser, token);

            return token;
        }

        public static bool isValidToken(string token)
        {
            User user = UserData.ValidateToken(token);

            //validar estatus y fecha ultimo login!

            return user != null && user.IDStatus == (int)Enums.UserStatus.Enabled;
        }
        public static User ValidateLogin(string username, string password)
        {
            User user = UserData.ValidateLogin(username, password);

            if (user != null && user.IDStatus == (int)Enums.UserStatus.Enabled)
            {
                user.Token = GenerateToken(user.IDUser);
            }


            return user;
        }
    }
}
