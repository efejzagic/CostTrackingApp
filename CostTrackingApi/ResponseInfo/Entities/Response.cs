﻿namespace ResponseInfo.Entities
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = "")
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
