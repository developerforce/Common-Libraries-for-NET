using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salesforce.Common
{
    enum DisplayTypes
    {
        Page,
        Popup,
        Touch,
        Mobile
    }

    public static class Common
    {
        public static string FormatUrl(string resourceName, string instanceUrl, string apiVersion)
        {
            return string.Format("{0}/services/data/{1}/{2}", instanceUrl, apiVersion, resourceName);
        }

        public static string FormatAuthUrl(
            string loginUrl,
            string responseType,
            string clientId,
            string redirectUrl,
            string display = "",
            string immediate = "",
            string state = "",
            string scope = "")
        {
            if (loginUrl == null) throw new ArgumentNullException("loginUrl");
            if (responseType == null) throw new ArgumentNullException("responseType");
            if (clientId == null) throw new ArgumentNullException("clientId");
            if (redirectUrl == null) throw new ArgumentNullException("redirectUrl");

            var url =
            string.Format(
                "{0}?response_type={1}&client_id={2}&redirect_uri={3}&display={4}&immediate={5}&state={6}&scope={7}",
                loginUrl,
                responseType,
                clientId,
                redirectUrl,
                display.ToLower(),
                immediate,
                state,
                scope);

            return url;
        }
    }
}
