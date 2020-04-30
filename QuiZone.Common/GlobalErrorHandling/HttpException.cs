using Newtonsoft.Json.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace QuiZone.Common.GlobalErrorHandling
{
    public class HttpException : System.Exception
    {
        
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"application/json"; // @text/plain

        public HttpException(HttpStatusCode StatusCode)
        {
            this.StatusCode = StatusCode;
        }

        public HttpException(HttpStatusCode StatusCode, string message) : base(message)
        {
            this.StatusCode = StatusCode;
        }

        public HttpException(HttpStatusCode statusCode, System.Exception inner) : this(statusCode, inner.ToString())
        { }

        public HttpException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        { }

        #region constructor serialization
        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected HttpException(SerializationInfo info, StreamingContext context)
        {
        }
        #endregion
    }
}
