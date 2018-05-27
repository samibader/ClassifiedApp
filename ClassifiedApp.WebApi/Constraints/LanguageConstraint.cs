using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ClassifiedApp.WebApi.Constraints
{
    public class LanguageConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                if (value.ToString().ToLower() == "en" || value.ToString().ToLower() == "ar")
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}