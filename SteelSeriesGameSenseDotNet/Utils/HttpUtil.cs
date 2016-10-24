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
        private static HttpClient _httpClient = new HttpClient();

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

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        ///// <summary>
        ///// Calls a GET request on the specified path.
        ///// </summary>
        ///// <param name="path">URL of the path to call as a string.</param>
        ///// <returns>HttpResponseMessage</returns>
        //public static async Task<HttpResponseMessage> GetAsync(string path)
        //{
        //    return _httpClient.GetAsync(path).Result;
        //}

        ///// <summary>
        ///// Calls a POST request on the specified path, posting the model data as JSON.
        ///// </summary>
        ///// <typeparam name="T">Type to return deserialised JSON as.</typeparam>
        ///// <param name="path">URL of the path to call as a string.</param>
        ///// <param name="data">Model data to be serialised as JSON.</param>
        ///// <returns></returns>
        //public static async Task<HttpResponseMessage> PostAsync<T>(string path, T data)
        //{
        //    var content = CreateStringContent(JsonService.SerializeObjectToJson(data));

        //    return _httpClient.PostAsync(path, content).Result;
        //}

        ///// <summary>
        ///// Calls a POST request on the specified path, passing data as HttpContent.
        ///// </summary>
        ///// <typeparam name="T">Type to return deserialised JSON as.</typeparam>
        ///// <param name="path">URL of the path to call as a string.</param>
        ///// <param name="data">Data to be sent in the POST request as HttpContent</param>
        ///// <param name="serialiseToJson">Whether or not to serialise as JSON. Calls <see cref="PostAsync{T}(string, T)"/> if true. Default: false.</param>
        ///// <returns></returns>
        //public static async Task<HttpResponseMessage> PostContentAsync<T>(string path, T data, bool serialiseToJson = false) where T : HttpContent
        //{
        //    if (serialiseToJson)
        //        return await PostAsync<T>(path, data);

        //    return _httpClient.PostAsync(path, data).Result;
        //}
    }
}