using ServiceApp.UI.Models.APIClient;
using ServiceApp.UI.Models.Response;
using ServiceApp.UI.Utils;
using System;
using System.Threading.Tasks;

namespace ServiceApp.UI.ApiClient
{
    public class APIClient : APIClientBase
    {
        private async Task<T> ErrorCheck<T>(Func<Task<APISuccessResponseModel<T>>> func)
        {
            var data = await func();

            if (data == null)
                throw new Exception("Data is NULL");

            if (data.StatusCode == ResponseStatusCode.Success)
            {
                return data.Data;
            }

            var msg = $"{data.StatusCode.ToString()}: {data.Message}";
            throw new Exception(msg);
        }

        public async Task<LinkResponse> GetLinkWithMeta(string hash)
        {
            var url = string.Format("{0}web-client/link", EnvConfig.APIUrl);
            return await ErrorCheck(async () => await ExecuteCall<APISuccessResponseModel<LinkResponse>>(url, "POST", new { Hash = hash }));
        }
    }
}
