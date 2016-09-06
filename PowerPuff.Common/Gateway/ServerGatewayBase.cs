using System;
using System.Net.Http;
using System.Text;
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

        protected Task<T> GetAsync<T>(string path)
        {
            return Execute<T>(httpClient => httpClient.GetAsync(ResolveFullUrl(path)));
        }

        protected Task<T> PutAsync<T>(string path, object parameter)
        {
            var httpContent = ToJsonContent(parameter);

            return Execute<T>(httpClient => httpClient.PutAsync(ResolveFullUrl(path), httpContent));
        }

        protected Task PutAsync(string path, object parameter)
        {
            var httpContent = ToJsonContent(parameter);

            return Execute(httpClient => httpClient.PutAsync(ResolveFullUrl(path), httpContent));
        }

        protected string ResolveFullUrl(string path)
        {
            var pathWithoutLeadingSlash = path.StartsWith("/") ? path.Substring(1) : path;
            return $"{_applicationSettings.ServerUrl}{pathWithoutLeadingSlash}";
        }

        private static async Task<T> Execute<T>(Func<HttpClient, Task<HttpResponseMessage>> action)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await action(httpClient).ConfigureAwait(false);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                return await DeserializeResponse<T>(response);
            }
        }

        private static StringContent ToJsonContent(object parameter)
        {
            var data = JsonConvert.SerializeObject(parameter);
            return new StringContent(data, Encoding.UTF8, "application/json");
        }

        private static async Task Execute(Func<HttpClient, Task<HttpResponseMessage>> action)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await action(httpClient).ConfigureAwait(false);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
        }

        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
