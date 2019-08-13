using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detention_facility.Custom
{
    public class ResponseClass<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T ResponseData { get; set; }

       /* public ResponseClass(bool status, string message, T data)
        {
            IsSuccess = status;
            Message = message;
            ResponseData = data;
        }*/
    }
}
