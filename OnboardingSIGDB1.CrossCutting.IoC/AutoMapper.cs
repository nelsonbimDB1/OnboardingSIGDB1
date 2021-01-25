using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.QueryResults;

namespace OnboardingSIGDB1.CrossCutting.IoC
{
    public class AutoMapperConfiguraction
    {
        public static void Config(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Funcionario, FuncionarioQueryResult>()
                    .ForMember(dst => dst.Cargo, opt => opt.Ignore());

                config.CreateMap<Cargo, CargoQueryResult>();

                config.CreateMap<Cargo, CargoFuncionarioQueryResult>();

                config.CreateMap<Empresa, EmpresaQueryResult>();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
