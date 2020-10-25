using MyWebFoundation.Framework.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace MyWebFoundation.Webserver.Controllers
{
    public class MyControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// ioc容器本质是反射，给你完整类型了，就可以创建了
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)ContainerFactory.CreateContainer().Resolve(controllerType);

            //return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}