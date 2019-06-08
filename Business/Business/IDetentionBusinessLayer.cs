using Detention_facility.Models;
using System;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public interface IDetentionBusinessLayer
    {
        void InsertDetention(Detention detention);
        void UpdateDetention(int id, Detention detention);
        void DeleteDetention(int id);
        Detention GetDetentionByID(int id);
        List<Detention> GetDetentions();
        List<Detention> GetDetentionsByPlace(string place);
        List<Detention> GetDetentionsByLastName(string lastname);
        List<Detention> GetDetentionsByDate(DateTime date);
    }
}