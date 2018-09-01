using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Host.Authentication
{
    internal sealed class AuthService
    {
        private static readonly Lazy<AuthService> lazy = new Lazy<AuthService>(() => new AuthService());

        public static AuthService Instance { get { return lazy.Value; } }

        public static bool IsAuthenticated { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static Task<HttpResponseMessage> hhtpResponse { get; set; }

        private AuthService()
        {
            Authentication.UserName = UserName;
            Authentication.Password = Password;
            hhtpResponse = Authentication.GetHttpResponse();
        }
    }
}
