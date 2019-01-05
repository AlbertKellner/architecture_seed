using DataEntity;

namespace ApiEndpoint
{
    using System.Reflection;
    using AutoMapper;
    using DataEntity.Model;
    using DataTransferObject;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using NJsonSchema;
    using NSwag.AspNetCore;
    using Provider;
    using Provider.Contracts;
    using Repository;
    using Repository.Contracts;
    using Repository.Operations;
    using Service;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            ConfigureIdentityService(services);

            ConfigureDependencyInjectionService(services);

            services.AddCors();

            services.AddAutoMapper();

            ConfigureJsonReturnService(services);
        }

        private static void ConfigureJsonReturnService(IServiceCollection services) => services.AddMvc(options =>
                                                                                                       {
                                                                                                           options.OutputFormatters.RemoveType<TextOutputFormatter>();
                                                                                                           options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                                                                                                       })
                                                                                               .AddJsonOptions(options => // Resolves a self referencing loop when converting EF Entities to Json
                                                                                                               {
                                                                                                                   options.SerializerSettings.ReferenceLoopHandling =
                                                                                                                       ReferenceLoopHandling.Ignore;
                                                                                                               });

        private void ConfigureIdentityService(IServiceCollection services)
        {
            services.AddDbContext<OnCareContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                                                                 b => b.MigrationsAssembly("AngularASPNETCore2WebApiAuth")));

            services.AddAuthorization(auth =>
                                      {
                                          auth.AddPolicy("Bearer",
                                                         new AuthorizationPolicyBuilder()
                                                             .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
                                      });
        }

        private static void ConfigureDependencyInjectionService(IServiceCollection services)
        {
            // Repositories Injection,
            services.AddScoped<IRepositoryFactory, UnitOfWork<OnCareContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<OnCareContext>>();
            services.AddScoped<IUnitOfWork<OnCareContext>, UnitOfWork<OnCareContext>>();

            // Services Injection
            services.AddTransient(typeof(IGenericProvider<UsuarioEntity>), typeof(UsuarioProvider));

            services.AddTransient(typeof(IGenericProviderDto<LaboratorioDto, LaboratorioEntity>), typeof(LaboratorioProvider));
            services.AddTransient(typeof(IGenericProviderDto<FarmaciaDto, FarmaciaEntity>), typeof(FarmaciaProvider));
            services.AddTransient(typeof(IGenericProviderDto<MedicoDto, TodoListEntity>), typeof(MedicoProvider));
            services.AddTransient(typeof(IGenericProviderDto<PacienteDto, PacienteEntity>), typeof(PacienteProvider));

            services.AddTransient(typeof(IGenericProviderDto<TaskDto, TaskEntity>), typeof(TaskProvider));
            services.AddTransient(typeof(IGenericProviderDto<TaskListDto, TaskListEntity>), typeof(TaskListProvider));

            services.AddTransient(typeof(IParentChildrenProviderDto<FarmaciaDto, FarmaciaEntity>), typeof(LaboratorioFarmaciaProvider));

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, OnCareContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            ConfigureSwagger(app);

            ConfigureCors(app);

            app.UseStaticFiles();

            app.UseMvc();
        }

        private static void ConfigureCors(IApplicationBuilder app) => app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        // Enable the Swagger UI middleware and the Swagger generator
        private static void ConfigureSwagger(IApplicationBuilder app) => app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly,
                                                                                          settings =>
                                                                                          {
                                                                                              settings.GeneratorSettings.DefaultPropertyNameHandling =
                                                                                                  PropertyNameHandling.CamelCase;
                                                                                          });
    }
}