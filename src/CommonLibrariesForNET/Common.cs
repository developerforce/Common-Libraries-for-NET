using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salesforce.Common
{
    public static class Common
    {
        public static string FormatUrl(string resourceName, string instanceUrl, string apiVersion)
        {
            return string.Format("{0}/services/data/{1}/{2}", instanceUrl, apiVersion, resourceName);
        }
    }
}
