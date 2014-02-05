using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salesforce.Common
{
    public enum DisplayTypes
    {
        Page,
        Popup,
        Touch,
        Mobile
    }

    public enum ResponseTypes
    {
        Code,
        Token
    }

    public static class Common
    {
        public static string FormatUrl(string resourceName, string instanceUrl, string apiVersion)
        {
            return string.Format("{0}/services/data/{1}/{2}", instanceUrl, apiVersion, resourceName);
        }

        public static string FormatAuthUrl(
            string loginUrl,
            ResponseTypes responseType,
            string clientId,
            string redirectUrl,
            DisplayTypes display = DisplayTypes.Page,
            bool immediate = false,
            string state = "",
            string scope = "")
        {
            if (loginUrl == null) throw new ArgumentNullException("loginUrl");
            if (clientId == null) throw new ArgumentNullException("clientId");
            if (redirectUrl == null) throw new ArgumentNullException("redirectUrl");

            var url =
            string.Format(
                "{0}?response_type={1}&client_id={2}&redirect_uri={3}&display={4}&immediate={5}&state={6}&scope={7}",
                loginUrl,
                responseType.ToString().ToLower(),
                clientId,
                redirectUrl,
                display.ToString().ToLower(),
                immediate.ToString(),
                state,
                scope);

            return url;
        }

        public static string FormatRefreshTokenUrl(
            string tokenRefreshUrl,
            string clientId,
            string refreshToken,
            string clientSecret = "")
        {
            if (tokenRefreshUrl == null) throw new ArgumentNullException("tokenRefreshUrl");
            if (clientId == null) throw new ArgumentNullException("clientId");
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");

            var clientSecretQuerystring = "";
            if (!string.IsNullOrEmpty(clientSecret))
            {
                clientSecretQuerystring = string.Format("&client_secret={0}", clientSecret);
            }

            var url =
            string.Format(
                "{0}?grant_type=refresh_token&client_id={1}{2}&refresh_token={3}",
                tokenRefreshUrl,
                clientId,
                clientSecretQuerystring,
                refreshToken);

            return url;
        }
    }
}
