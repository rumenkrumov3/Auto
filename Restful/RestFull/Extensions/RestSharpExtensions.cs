using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestFul.Extensions
{
    public static class RestSharpExtensions
    {
        public static bool isSuccessfulStatusCode(this HttpStatusCode responseCode)
        {
            var code = (int)responseCode;

            int statusCodeOk = (int)HttpStatusCode.OK;
            int statusCodeBadRequest = (int)HttpStatusCode.BadRequest;

            return code >= statusCodeOk && code < statusCodeBadRequest;    
        }

        public static bool IsSuccessful(this RestResponse restResponse)
        => restResponse.StatusCode == HttpStatusCode.OK &&
           restResponse.ResponseStatus == ResponseStatus.Completed;
    }
}
