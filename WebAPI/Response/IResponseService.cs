using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Response
{
    interface IResponseService<T>
    {
        
        ResponseClass<T> CreateResponse(string message, bool status, T data);
    }
}
