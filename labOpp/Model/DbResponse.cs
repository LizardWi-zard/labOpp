using System.Net;

namespace labOpp.Model
{
    public class DbResponse
    {
        public HttpStatusCode Status { get; set; }

        public object? Data { get; set; }
    }
}
