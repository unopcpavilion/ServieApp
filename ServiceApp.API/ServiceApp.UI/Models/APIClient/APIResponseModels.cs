using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.UI.Models.APIClient
{
    public class APIBaseResponseModel
    {
        public ResponseStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class APISuccessResponseModel<T> : APIBaseResponseModel
    {
        public T Data { get; set; }
    }
}
