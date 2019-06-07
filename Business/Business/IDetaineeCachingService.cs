using Detention_facility.Models;

namespace Detention_facility.Business
{
    public interface IDetaineeCachingService
    {
        Detainee Get(int id);
        bool Add(Detainee detainee);
        void Update(Detainee detainee);
        void Delete(int id);

    }
}
