using Microsoft.EntityFrameworkCore;

namespace ApiEndpoint
{
    using DataEntity;
    using DataEntity.Model;
    using DataTransferObject;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Provider;
    using Provider.Contracts;
    using Repository;
    using Repository.Contracts;
    using Repository.Operations;
    using Service;
    using AutoMapper;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            services.AddRouting();

            services.AddControllers();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddHttpContextAccessor();

            services.AddCors();

            ConfigureDependencyInjectionService(services);

            ConfigureSwagger(services);

            ConfigureJsonReturnService(services);
        }

        private void ConfigureDatabase(IServiceCollection services) =>
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        private static void ConfigureJsonReturnService(IServiceCollection services)
        {
            services.AddMvc(options =>
                                    {
                                        options.OutputFormatters.RemoveType<TextOutputFormatter>();
                                        options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                                    })
                                    .AddJsonOptions(options => // Resolves a self referencing loop when converting EF Entities to Json
                                    {
                                        options.JsonSerializerOptions.MaxDepth = 3;
                                        //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                    });
        }

        private static IServiceCollection ConfigureSwagger(IServiceCollection services) =>
            services.AddSwaggerDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "Sample API";
                c.Version = "v1";
                c.Description = "The sample API documentation description.";
            });

        private static void ConfigureDependencyInjectionService(IServiceCollection services)
        {
            // Repositories Injection,
            services.AddScoped<IRepositoryFactory, UnitOfWork<DatabaseContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<DatabaseContext>>();
            services.AddScoped<IUnitOfWork<DatabaseContext>, UnitOfWork<DatabaseContext>>();

            // Services Injection
            services.AddTransient(typeof(IGenericProvider<UsuarioEntity>), typeof(UsuarioProvider));

            services.AddTransient(typeof(IGenericProviderDto<LaboratorioDto, LaboratorioEntity>), typeof(LaboratorioProvider));
            services.AddTransient(typeof(IGenericProviderDto<FarmaciaDto, FarmaciaEntity>), typeof(FarmaciaProvider));

            services.AddTransient(typeof(IParentChildrenProviderDto<FarmaciaDto, FarmaciaEntity>), typeof(LaboratorioFarmaciaProvider));

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            ConfigureCors(app);

            app.UseStaticFiles();

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureCors(IApplicationBuilder app) => app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    }
}