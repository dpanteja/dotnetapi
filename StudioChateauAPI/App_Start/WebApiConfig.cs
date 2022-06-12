using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using StudioChateauAPI.Models;
using System.Web.Http.OData.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace StudioChateauAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Communities>("Communities");
            builder.EntitySet<Phase>("Phases");
            builder.EntitySet<Plan>("Plans");
            builder.EntitySet<Lot>("Lots");
            builder.EntitySet<XMLDump>("XMLDumps");
            builder.EntitySet<StudioChateauAPIErrorLog>("StudioChateauAPIErrorLogs");
            builder.EntitySet<ExternalServices>("ExternalServices");
            builder.EntitySet<ApplicationUser>("ApplicationUsers");
            builder.EntitySet<IdentityUserClaim>("IdentityUserClaims");
            builder.EntitySet<IdentityUserLogin>("IdentityUserLogins");
            builder.EntitySet<IdentityUserRole>("IdentityUserRoles");
            

            config.Routes.MapODataServiceRoute("ProjectConfig", "ProjectConfig",builder.GetEdmModel());

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new IsoDateTimeConverter());
            config.Formatters.JsonFormatter.SerializerSettings = serializerSettings;
        }
    }
}
