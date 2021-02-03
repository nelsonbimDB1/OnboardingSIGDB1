using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Services.Cargo;
using OnboardingSIGDB1.Domain.Interfaces.Services.Empresa;
using OnboardingSIGDB1.Domain.Interfaces.Services.Funcionario;
using OnboardingSIGDB1.Domain.Notification;
using OnboardingSIGDB1.Domain.Services;
using System.Linq;

namespace OnboardingSIGDB1.CrossCutting.IoC
{
    public class ConfigurationIOC
    {
        public static void LoadDomainServices(IServiceCollection service)
        {
            service.AddScoped<IArmazenadorCargo, ArmazenadorCargo>();
            service.AddScoped<IRemocaoCargo, RemocaoCargo>();
            service.AddScoped<IArmazenadorEmpresa, ArmazenadorEmpresa>();
            service.AddScoped<IRemocaoEmpresa, RemocaoEmpresa>();
            service.AddScoped<IArmazenadorFuncionario, ArmazenadorFuncionario>();
            service.AddScoped<IRemocaoFuncionario, RemocaoFuncionario>();
            service.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        }

        public static void LoadDataServices(IServiceCollection services)
        {
            var domainServiceAssembly = typeof(ScopedDataRegister).Assembly;
            var domainServiceRegistrations =
                from type in domainServiceAssembly.GetExportedTypes()
                where type.BaseType == typeof(ScopedDataRegister)
                select new { Services = type.GetInterfaces(), Implementation = type };

            foreach (var reg in domainServiceRegistrations)
            {
                if (reg.Services.Any())
                {
                    foreach (var service in reg.Services)
                    {
                        services.AddScoped(service, reg.Implementation);
                    }
                }
                else
                {
                    services.AddScoped(reg.Implementation);
                }
            }

        }
    }
}
