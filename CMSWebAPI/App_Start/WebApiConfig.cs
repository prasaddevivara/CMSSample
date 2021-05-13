﻿using CMSSample.DA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace CMSWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var container = new UnityContainer();
            //container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            //config.DependencyResolver = new UnityResolver(container);
        }
    }
}
