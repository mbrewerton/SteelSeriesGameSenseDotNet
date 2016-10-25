using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SteelSeriesGameSenseDotNet.Models;

namespace SteelSeriesGameSenseDotNet.Utils
{
    public static class HttpUtil
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static void SetupHttpUtil()
        {
            var resolver = new DefaultContractResolver();
            resolver.NamingStrategy = new SnakeCaseNamingStrategy(true, true);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                ContractResolver = resolver
            };
            
            var path = Path.GetFullPath($"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}{ConfigUtil.CorePropsWindows}");
            using (StreamReader reader = new StreamReader(path))
            {
                CoreProps coreProps = JsonConvert.DeserializeObject<CoreProps>(reader.ReadToEnd());
                _httpClient.BaseAddress = new Uri($"http://{coreProps.Address}/");
            }
        }

        private static StringContent CreateStringContent<T>(T data)
        {
            var content = JsonConvert.SerializeObject(data);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Calls an async GET request to the given path.
        /// </summary>
        /// <param name="path">URL of the path as string.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;</returns>
        public static async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await _httpClient.GetAsync(path);
        }

        /// <summary>
        /// Calls an async GET request to the given path.
        /// </summary>
        /// <param name="path">URL of the path as string.</param>
        /// <param name="data">Your data object to post.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;</returns>
        public static async Task<HttpResponseMessage> PostAsync<T>(string path, T data)
        {
            return await _httpClient.PostAsync(path, CreateStringContent(data));
        }
    }
}