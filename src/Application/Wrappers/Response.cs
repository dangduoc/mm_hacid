using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrappers
{
    public class Response<T> : Response
    {
        public T Data { get; set; }
        public Response()
        {

        }

        public Response(bool status, string message = null, T data = default(T))
            : base(status, message)
        {
            Data = data;
        }
    }
    public class Response
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public IDictionary<string, string> Errors { get; set; }

        public Response()
        {

        }

        public Response(bool status, string message = null)
        {
            Status = status;
            Message = message;
        }

        public void SetSuccess()
        {
            Status = true;
        }

        public void SetFail()
        {
            Status = false;
        }

        public void AddMessageError(string key, string message)
        {
            Errors[key] = message;
        }
    }
}
