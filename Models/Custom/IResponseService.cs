using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detention_facility.Custom
{
    public interface IResponseService<T>
    {
        ResponseClass<T> CreateResponse(string message, bool status, T data);
    }
}
