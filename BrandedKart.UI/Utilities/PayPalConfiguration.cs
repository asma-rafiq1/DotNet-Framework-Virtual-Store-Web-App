using PayPal.Api;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;

namespace BrandedKart.UI.Utilities
{
    public class PayPalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PayPalConfiguration()
        {
            var config = GetConfig();
            clientId = "";
            clientSecret = "";
        }

        private static Dictionary<string,string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetApiContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }

}