using Aurora.Models.Derived;
using Aurora.Models.Primitives;
using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.Routing;

namespace Aurora
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Company>("Company");
            modelBuilder.EntitySet<HiringProfile>("HiringProfile");
            modelBuilder.EntitySet<Department>("Departments");
            modelBuilder.EntitySet<User>("Users");
            modelBuilder.EntitySet<Student>("Students");
            modelBuilder.EntitySet<Campus>("Campuses");
            modelBuilder.EntitySet<Role>("Roles");
            modelBuilder.EntitySet<Permission>("Permissions");
            modelBuilder.EntitySet<ProcessStep>("ProcessSteps");
            modelBuilder.EntitySet<StudentProgression>("StudentProgression");
            modelBuilder.EntitySet<Resource>("Resources");

            IEdmModel model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute(routeName: "OData", routePrefix: "odata", model: model);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApiRoute",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}