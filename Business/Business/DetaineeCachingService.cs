using System;
using System.Runtime.Caching;
using Detention_facility.Models;

namespace Detention_facility.Business
{
    public class DetaineeCachingService : IDetaineeCachingService
    {
        public Detainee Get(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(id.ToString()) as Detainee;
        }

        public bool Add(Detainee detainee)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(detainee.DetaineeID.ToString(), detainee, DateTime.Now.AddMinutes(2));
        }

        public void Update(Detainee detainee)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(detainee.DetaineeID.ToString(), detainee, DateTime.Now.AddMinutes(2));
        }

        public void Delete(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(id.ToString()))
            {
                memoryCache.Remove(id.ToString());
            }
        }
    }
}