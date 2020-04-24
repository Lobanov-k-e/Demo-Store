using Microsoft.AspNetCore.Http;

namespace SportStore.WebUi.Common.UrlExtensions
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Get request path and query as raw url string
        /// </summary>       
        /// <returns>return url string</returns>
        public static string GetPathAndQuery(this HttpRequest request) => request.QueryString.HasValue ?
            $"{request.Path}{request.QueryString}" 
            : request.Path.ToString();
    }
}
