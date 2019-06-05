using Detention_facility.Business;
using System.Web.Http;
using Unity;
using Unity.WebApi;
namespace Detention_facility
{
    public static class UnityConfig
    {
        public static void RegisterTypes()
        {
            var container = new UnityContainer();

            container.AddNewExtension<DependencyInjectionExtension>();
            container.RegisterType<IDeliveryBusinessLayer, DeliveryBusinessLayer>();
            container.RegisterType<IDetaineeBusinessLayer, DetaineeBusinessLayer>();
            container.RegisterType<IEmployeeBusinesslayer, EmployeesBusinessLayer>();
            container.RegisterType<IDetentionBusinessLayer, DetentionBusinessLayer>();
            container.RegisterType<IReleaseBusinessLayer, ReleaseBusinessLayer>();
            container.AddNewExtension<DependencyInjectionExtension>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}