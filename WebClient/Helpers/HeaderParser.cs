using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebClient.Helpers;

public class HeaderParser
{
    public static PaginationInfo? 
        FindAndParsePaginationInfo(HttpResponseHeaders responseHeaders)
    {
        if (responseHeaders.Contains("X-Pagination"))
        {
            var xPag = responseHeaders.First(rh => rh.Key == "X-Pagination").Value;
            return JsonConvert.DeserializeObject<PaginationInfo>(xPag.First());
        }

        return null;
    }
}
