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
            string decrypted = string.Empty;

            try
            {
                if (UVmodel != null)
                {
                    User loginUser = UVmodel.GetUserDataToCompare();

                    decrypted = RijndaelEncryption.DecryptRijndael(loginUser.Password, loginUser.Salt);

                    if (decrypted == UVmodel.LoginUser.Password)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }
                
            return false;
        }
    }
}