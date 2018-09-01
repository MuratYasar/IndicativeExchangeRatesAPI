using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndicativeExchangeRates.Authentication.WebAPI
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {
            using (Models.AuthenticationEntities entity = new Models.AuthenticationEntities())
            {
                return entity.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}