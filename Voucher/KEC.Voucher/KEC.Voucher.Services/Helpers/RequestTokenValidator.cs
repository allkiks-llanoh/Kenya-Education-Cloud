using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace KEC.Voucher.Services.Helpers
{
   public static class RequestTokenValidator
    {
        public static void ValidateRequestHeader(HttpRequestMessage request)
        {
            string cookieToken = string.Empty;
            string formToken = string.Empty;
            if (request.Headers.TryGetValues("RequestVerificationToken", out IEnumerable<string> tokenHeaders))
            {
                var tokens = tokenHeaders.First().Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }
            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}
