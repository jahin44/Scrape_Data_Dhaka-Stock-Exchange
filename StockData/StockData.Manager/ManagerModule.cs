using Autofac;
using StockData.Manager.Contexts;
using StockData.Manager.Repositories;
using StockData.Manager.Services;
using StockData.Manager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager
{
    public class ManagerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ManagerModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockDataDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StockDataDbContext>().As<IStockDataDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
    
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockPriceRepository>().As<IStockPriceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ManagerUnitOfWork>().As<IManagerUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockPriceService>().As<IStockPriceService>()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
