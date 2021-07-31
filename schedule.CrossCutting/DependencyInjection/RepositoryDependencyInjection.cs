using Microsoft.Extensions.DependencyInjection;
using Schedule.Data.Repositories;
using Schedule.domain.Interfaces.Repositories;

namespace schedule.crossCutting.DependencyInjection
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
