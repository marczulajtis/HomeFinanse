using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Models
{
    public static class PasswordAuthentication
    {
        public static bool UserIsValid(UserViewModel UVmodel)
        {
            User loginUser = UVmodel.GetUserDataToCompare();

            string decrypted = RijndaelEncryption.DecryptRijndael(loginUser.Password, loginUser.Salt);

            if (decrypted == UVmodel.LoginUser.Password)
            {
                return true;
            }

            return false;
        }
    }
}