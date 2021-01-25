using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Repositories;
using OnboardingSIGDB1.Data.UoW;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.UoW;
using OnboardingSIGDB1.Domain.Notification;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.CrossCutting.IoC
{
    public class ConfigurationIOC
    {
        public static void LoadServices(IServiceCollection service)
        {
            #region 

            #region IOC Domain Services
            service.AddScoped<ICargoService, CargoService>();
            service.AddScoped<IEmpresaService, EmpresaService>();
            service.AddScoped<IFuncionarioService, FuncionarioService>();
            #endregion

            #region IOC Domain Notification
            service.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
            #endregion

            #region IOC Repositories
            service.AddScoped<ICargoRepository, CargoRepository>();
            service.AddScoped<IEmpresaRepository, EmpresaRepository>();
            service.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            #endregion

            #region IOC UoW
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #endregion

        }
    }
}
