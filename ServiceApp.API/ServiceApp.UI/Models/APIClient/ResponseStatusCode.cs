using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.UI.Models.APIClient
{
    public enum ResponseStatusCode
    {
        Success = 200,
        BadRequest = 400,
        NotFound = 404,
        BlockedRequest = 407,
        ExpirationDateData = 409,
        UniqRowDuplicate = 410,
        CanNotAdd = 418,
        CanNotDelete = 420,
        Error = 500
    }
}
