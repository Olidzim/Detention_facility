namespace Detention_facility.Data
{
    public static class Constants
    {

        //Delivery
        public const string InsertDelivery = "InsertDelivery";
        public const string GetDeliveryByID = "GetDeliveryByID";
        public const string GetDeliveriesByIDs = "GetDeliveriesByIDs";
        public const string GetSmartDeliveriesByIDs = "GetSmartDeliveriesByIDs";
        public const string GetDeliveriesOfDetainees = "GetDeliveriesOfDetainees";
        public const string UpdateDelivery = "UpdateDelivery";
        public const string DeleteDelivery = "DeleteDelivery";

        public const string DeliveryID = "@DeliveryID";
        public const string DeliveredByEmployeeID = "@DeliveredByEmployeeID";
        public const string DeliveryDate = "@DeliveryDate";


        //Detainee
        public const string InsertDetainee = "InsertDetainee";
        public const string AddDetaineeToDetention = "AddDetaineeToDetention";
        public const string CheckDetaineeInDetentionearch = "CheckDetaineeInDetention";
        public const string GetDetaineesByDetentionID = "GetDetaineesByDetentionID";
        public const string GetDetaineeByID = "GetDetaineeByID";
        public const string GetLastDetaineeID = "GetLastDetaineeID";
        public const string UpdateDetainee = "UpdateDetainee";
        public const string DetaineeSearch = "DetaineeSearch";
        public const string DeleteDetainee = "DeleteDetainee";
        public const string DetaineeSearchByAddres = "DetaineeSearchByAddress";
        public const string GetDetainees = "GetDetainees";

        public const string DetaineeID = "@DetaineeID";
        public const string FirstName = "@FirstName";
        public const string LastName = "@LastName";
        public const string Patronymic = "@Patronymic";
        public const string Birthdate = "@Birthdate";
        public const string MaritalStatus = "@MaritalStatus";
        public const string Job = "@Job";
        public const string MobilePhoneNumber = "@MobilePhoneNumber";
        public const string HomePhoneNumber = "@HomePhoneNumber";
        public const string Photo = "@Photo";
        public const string ExtraInfo = "@ExtraInfo";
        public const string ResidencePlace = "@ResidencePlace";


        //Detention
        public const string InsertDetention = "InsertDetention";
        public const string GetDetentionByID = "GetDetentionByID";
        public const string GetDetentions = "GetDetentions";
        public const string GetSmartDetentions = "GetSmartDetentions";
        public const string GetLastDetentionID = "GetLastDetentionID";
        public const string GetSmartDetentionsByDetaineeID = "GetSmartDetentionsByDetaineeID";
        public const string GetSmartDetentionsByDetentionID = "GetSmartDetentionsByDetentionID";
        public const string GetDetentionsByResidencePlace = "GetDetentionsByResidencePlace";
        public const string GetDetentionsByLastName = "GetDetentionsByLastName";
        public const string GetSmartDetentionsByDate = "GetSmartDetentionsByDate";
        public const string UpdateDetention = "UpdateDetention";
        public const string DeleteDetention = "DeleteDetention";

        public const string DetentionID = "@DetentionID";
        public const string DetentionDate = "@DetentionDate";
        public const string DetainedByEmployeeID = "@DetainedByEmployeeID";
        public const string PlaceAddress = "@PlaceAddress";


        //Employee
        public const string GetEmployeeByID = "GetEmployeeByID";
        public const string GetEmployees = "GetEmployees";
        public const string EmployeeSearch = "EmployeeSearch";
        public const string InsertEmployee = "InsertEmployee";
        public const string UpdateEmployee = "UpdateEmployee";
        public const string DeleteEmployee = "DeleteEmployee";

        public const string EmployeeID = "@EmployeeID";
        public const string Position = "@Position";
        public const string EmployeeRank = "@EmployeeRank";


        //Release
        public const string InsertRelease = "InsertRelease";
        public const string GetReleasesByIDs = "GetReleasesByIDs";
        public const string GetReleasesOfDetainees = "GetReleasesOfDetainees";
        public const string UpdateRelease = "UpdateRelease";
        public const string DeleteRelease = "DeleteRelease";
        public const string GetReleaseByID = "GetReleaseByID";

        public const string ReleaseID = "@ReleaseID";
        public const string ReleasedByEmployeeID = "@ReleasedByEmployeeID";
        public const string ReleaseDate = "@ReleaseDate";
        public const string AmountPaid = "@AmountPaid";
        public const string AmountAccrued = "@AmountAccrued";

        //Common
        public const string term = "@term";



    }
}
