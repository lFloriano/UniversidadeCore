using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;
using Universidade.Infrastructure.Repositorio;

namespace Universidade.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
            services.AddScoped<IEstudanteRepository, EstudanteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<UniversidadeContext>(opt => opt.UseSqlServer(connectionString));

            return services;
        }
    }
}
