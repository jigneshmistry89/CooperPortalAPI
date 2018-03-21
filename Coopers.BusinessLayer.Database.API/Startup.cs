using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Autofac;
using System.Web.Http;
using System.Reflection;
using Autofac.Integration.WebApi;
using Coopers.BusinessLayer.Database.Repositories.Repository;
using System.Data.Entity;
using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain;

[assembly: OwinStartup(typeof(Coopers.BusinessLayer.Database.API.Startup))]

namespace Coopers.BusinessLayer.Database.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType(typeof(NetworkLocationRepository)).AsImplementedInterfaces();
            builder.RegisterType(typeof(PaymentHistoryRepository)).AsImplementedInterfaces();
            builder.RegisterType(typeof(TaxableStatesRepository)).AsImplementedInterfaces();
            builder.RegisterType(typeof(AccountLocationRepository)).AsImplementedInterfaces();

            builder.RegisterType<CooperAtkinEntities>().As<DbContext>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerRequest();

            var container = builder.Build();

            ConfigureAuth(app);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
