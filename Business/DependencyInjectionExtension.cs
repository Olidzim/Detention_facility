using Detention_facility.Data;
using Unity;
using Unity.Extension;
namespace Detention_facility.Business
{
    public class DependencyInjectionExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {           
            Container.RegisterType<IDeliveryDataAccess, DeliveryDataAccessLayer>();
            Container.RegisterType<IDetaineeDataAccess, DetaineeDataAccessLayer>();
            Container.RegisterType<IEmployeeDataAccess, EmployeeDataAccessLayer>();
            Container.RegisterType<IDetentionDataAccess, DetentionDataAccessLayer>();
            Container.RegisterType<IReleaseDataAccess, ReleaseDataAccessLayer>();
        }
    }
}