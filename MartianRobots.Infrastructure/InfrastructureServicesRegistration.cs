using MartianRobots.Application.Interfaces;
using MartianRobots.Infrastructure.Formatters;
using MartianRobots.Infrastructure.Parsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IInputParser, InputParser>();
            services.AddTransient<IOutputFormatter, OutputFormatter>();

            return services;
        }
    }
}
