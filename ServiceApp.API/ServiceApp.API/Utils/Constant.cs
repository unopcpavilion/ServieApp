namespace ServiceApp.API.Utils
{
    public class Constant
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
}
