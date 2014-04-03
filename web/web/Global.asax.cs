using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Extensions.Conventions;
using Web.Mappings;

namespace Web
{
    using System.Data.Entity;

    using Model.Repository;

    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            CreateMaps.Create();
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            //Autowriting
            kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().BindDefaultInterfaces());
            kernel.Bind<IGenericRepository>().To<GenericRepository>();
            kernel.Bind<DbContext>().To<Model.Model1Container>();
            return kernel;
        }


    }
}
