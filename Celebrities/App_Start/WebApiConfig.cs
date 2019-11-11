using Celebrities.DAL;
using Celebrities.Mappers;
using Celebrities.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Unity;
using Unity.Lifetime;

namespace Celebrities
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IRepository<Celebrity>, CelebrityRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMapper<Celebrity>, CelebrityMapper>(new HierarchicalLifetimeManager());
            
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithId", 
                routeTemplate: "Api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional }, 
                constraints: new { id = @"\d+" });
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction", 
                routeTemplate: "Api/{controller}/{action}");
            config.Routes.MapHttpRoute(
                name: "DefaultApiGet", 
                routeTemplate: "Api/{controller}", 
                defaults: new { action = "Get" }, 
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute(
                name: "DefaultApiPost", 
                routeTemplate: "Api/{controller}", 
                defaults: new { action = "Post" }, 
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
        }
    }
}
