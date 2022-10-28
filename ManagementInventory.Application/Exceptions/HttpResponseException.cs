using System.Net;

namespace ManagementInventory.Application.Exceptions
{
    /// <summary>
    /// Class to create a http response exception
    /// </summary>
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="value"></param>
        public HttpResponseException(HttpStatusCode statusCode, object? value = null) =>
            (StatusCode, Value) = (statusCode, value);

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// 
        /// </summary>
        public object? Value { get; }
    }
}