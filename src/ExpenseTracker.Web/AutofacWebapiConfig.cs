using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExpenseTracker.Data;
using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web
{
    public class AutofacWebapiConfig
    {
        //public static IContainer Container
        //{
        //    get { return builder.Build(); }
        //}

        static ContainerBuilder builder;

        public static IContainer Initialize(IServiceCollection services)
        {
            builder = new ContainerBuilder();
            RegisterServices(services);
            return builder.Build();
        }

        static void RegisterServices(IServiceCollection services)
        {

            #region Data layer
            builder.RegisterType<ExpenseTrackerContext>()
                            .As<DbContext>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>()
                            .As<IDbFactory>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>()
                            .As<IUnitofWork>()
                            .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                            .As(typeof(IEntityBaseRepository<>))
                            .InstancePerLifetimeScope();

            #endregion

            #region Services
            builder.RegisterType<EncryptionService>()
                            .As<IEncryptionService>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<MembershipService>()
                            .As<IMembershipService>()
                            .InstancePerLifetimeScope();

            #endregion 

            builder.Populate(services);

        }
    }
}
