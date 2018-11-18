using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMoviesApi.Utils
{
    public static class NetworkHelper
    {
        public async static Task<string> GetPageHtml(string url)
        {
            string HtmlText;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync(url);
                var httpContent = await httpResponse.Content.ReadAsByteArrayAsync();
                string responseString = Encoding.GetEncoding(1251).GetString(httpContent, 0, httpContent.Length);
                HtmlText = responseString;
            }
            return HtmlText;
        }
    }
}
