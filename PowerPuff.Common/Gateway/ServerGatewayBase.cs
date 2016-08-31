using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PowerPuff.Common.Settings;

namespace PowerPuff.Common.Gateway
{
    public class ServerGatewayBase
    {
        private readonly IApplicationSettings _applicationSettings;

        public ServerGatewayBase(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        protected async Task<T> GetAsync<T>(string path)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(ResolveFullUrl(path)).ConfigureAwait(false);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        protected string ResolveFullUrl(string path)
        {
            var pathWithoutLeadingSlash = path.StartsWith("/") ? path.Substring(1) : path;
            return $"{_applicationSettings.ServerUrl}{pathWithoutLeadingSlash}";
        }
    }
}
