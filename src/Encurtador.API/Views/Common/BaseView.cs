using System;
using System.Net;

namespace Encurtador.API.Views.Common
{
    public class BaseView<T> where T : class
    {
        public BaseView(HttpStatusCode httpStatus, string message, T? data)
        {
            StatusCode = (int)httpStatus;
            Message = message;
            Data = data;
        }

        public BaseView(HttpStatusCode httpStatus, string message, T? data, string erro)
        {
            StatusCode = (int)httpStatus;
            Message = message;
            Data = data;
            Error.Add(erro);
        }

        public BaseView(HttpStatusCode httpStatus, string message, T? data, List<string> erros)
        {
            StatusCode = (int)httpStatus;
            Message = message;
            Data = data;
            Error.AddRange(erros);
        }

        public int StatusCode {
            get;
            private set;
        }

        public string Message{
            get;
            private set;
        }

        public T? Data{
            get;
            private set;
        }

        public List<string> Error
        {
            get;
            private set;
        } = new List<string>();
    }
}

