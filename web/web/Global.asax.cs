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
using System.Configuration;

namespace Web
{
    using System.Data.Entity;

    using Model.Repository;
    using Ninject.Activation;
    using Web.Attributes;

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
            kernel.Bind<String>().ToProvider<ConfigProvider>().WhenTargetHas<ConfigDependencyAttribute>();
            return kernel;
        }
    }

    public class ConfigProvider : Provider<object>
    {
        protected override object CreateInstance(IContext context)
        {
            var settingName = context.Request.Target.Name;
            var type = context.Request.Target.Type;
            return Convert.ChangeType(System.Configuration.ConfigurationManager.AppSettings[settingName], type);
        }
    }
}
