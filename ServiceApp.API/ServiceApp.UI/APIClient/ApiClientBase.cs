using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.UI.ApiClient
{
    public abstract class APIClientBase
    {
        private const int ProtocolError = 7;

        protected async Task<T> ExecuteCall<T>(string url, string method, object body = null) where T : class
        {
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = method;

            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                var bytes = Encoding.UTF8.GetBytes(json);

                using (var dataStream = await request.GetRequestStreamAsync())
                {
                    await dataStream.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception($"Error fetching data. Status code: {response.StatusCode}");

                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrEmpty(content))
                            return null;

                        return JsonConvert.DeserializeObject<T>(content);
                    }
                }
            }
            catch (WebException ex)
            {
                var content = "";
                if ((int)ex.Status == ProtocolError && ex.Response != null)
                {
                    try
                    {
                        var response = (HttpWebResponse)ex.Response;
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            content = reader.ReadToEnd();

                            return JsonConvert.DeserializeObject<T>(content);
                        }
                    }
                    catch
                    {
                    }
                }

                var exToThrow = !string.IsNullOrEmpty(content) ? new Exception(content, ex) : ex;

                var msg = $"APIClientBase ExecuteCall throws exception: {ex.Message}";

                throw exToThrow;
            }
        }
    }
}
