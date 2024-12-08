using Application.Interfaces.RepositoryInterfaces;
using Infrastructure.Database;
using Infrastructure.DatabaseSeeder;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<FakeDatabase>();

            services.AddDbContext<RealDatabase>(options =>
            {
                options.UseSqlServer(connectionString);

            });

            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<UserSeeder>();
            services.AddTransient<BookSeeder>();
            services.AddTransient<AuthorSeeder>();

            return services;
        }
    }
}
