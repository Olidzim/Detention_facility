using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Response
{
    public class ResponseService<T> : IResponseService<T>
    {
        public ResponseClass<T> CreateResponse(string message, bool status, T data)
        {
            ResponseClass<T> response = new ResponseClass<T>(status, message, data);
            return response;
        }        
    }
}