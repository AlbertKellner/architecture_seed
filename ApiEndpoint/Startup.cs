namespace ApiEndpoint
{
    using System;
    using System.Reflection;
    using System.Text;
    using Auth;
    using AutoMapper;
    using DataEntity.Model;
    using DataTransferObject;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using Newtonsoft.Json;
    using NJsonSchema;
    using NSwag.AspNetCore;
    using Provider;
    using Provider.Contracts;
    using Repository;
    using Repository.Contracts;
    using Repository.Operations;
    using Service;

    //using Elmah.Io.AspNetCore;

    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            ConfigureIdentityService(services);

            //services.Configure<ElmahIoOptions>(Configuration.GetSection("ElmahIo"));

            ConfigureDependencyInjectionService(services);

            services.AddCors();

            services.AddAutoMapper();

            ConfigureJsonReturnService(services);

            ConfigureJwtIssuerOptions(services);

            ConfigureAuthenticationPipeline(services);
        }

        private void ConfigureAuthenticationPipeline(IServiceCollection services)
        {
            // jwt wire up
            // GetByIdentity options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            var tokenValidationParameters = new TokenValidationParameters
                                            {
                                                ValidateIssuer = true,
                                                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                                                ValidateAudience = true,
                                                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                                                ValidateIssuerSigningKey = true,
                                                IssuerSigningKey = _signingKey,
                                                RequireExpirationTime = false,
                                                ValidateLifetime = true,
                                                ClockSkew = TimeSpan.Zero
                                            };

            services.AddAuthentication(options =>
                                       {
                                           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                       }).AddJwtBearer(configureOptions =>
                                                       {
                                                           configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                                                           configureOptions.TokenValidationParameters = tokenValidationParameters;
                                                           configureOptions.SaveToken = true;
                                                       });

            // api user claim policy
            services.AddAuthorization(options =>
                                      {
                                          options.AddPolicy("ApiUser",
                                                            policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
                                      });

            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
                                                            {
                                                                // configure identity options
                                                                o.Password.RequireDigit = false;
                                                                o.Password.RequireLowercase = false;
                                                                o.Password.RequireUppercase = false;
                                                                o.Password.RequireNonAlphanumeric = false;
                                                                o.Password.RequiredLength = 2;
                                                            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<OnCareContext>().AddDefaultTokenProviders();
        }

        private void ConfigureJwtIssuerOptions(IServiceCollection services)
        {
            // GetByIdentity options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
                                                 {
                                                     options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                                                     options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                                                     options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
                                                 });
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

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<OnCareContext>().AddDefaultTokenProviders();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            //var tokenConfigurations = new TokenConfigurations();
            //new ConfigureFromConfigurationOptions<TokenConfigurations>(
            //        Configuration.GetSection("TokenConfigurations"))
            //    .Configure(tokenConfigurations);
            //services.AddSingleton(tokenConfigurations);

            //services.AddAuthentication(authOptions =>
            //{
            //    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationSch'eme;
            //    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions =>
            //{
            //    var paramsValidation = bearerOptions.TokenValidationParameters;
            //    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            //    paramsValidation.ValidAudience = tokenConfigurations.Audience;
            //    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            //    // Valida a assinatura de um token recebido
            //    paramsValidation.ValidateIssuerSigningKey = true;

            //    // Verifica se um token recebido ainda é válido
            //    paramsValidation.ValidateLifetime = true;

            //    // Tempo de tolerância para a expiração de um token (utilizado
            //    // caso haja problemas de sincronismo de horário entre diferentes
            //    // computadores envolvidos no processo de comunicação)
            //    paramsValidation.ClockSkew = TimeSpan.Zero;
            //});

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
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
            services.AddTransient(typeof(IGenericProviderDto<MedicoDto, MedicoEntity>), typeof(MedicoProvider));
            services.AddTransient(typeof(IGenericProviderDto<PacienteDto, PacienteEntity>), typeof(PacienteProvider));

            services.AddTransient(typeof(IParentChildrenProviderDto<FarmaciaDto, FarmaciaEntity>), typeof(LaboratorioFarmaciaProvider));

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            services.AddSingleton<IJwtFactory, JwtFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, OnCareContext context, UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            ConfigureSwagger(app);

            ConfigureCors(app);

            app.UseStaticFiles();

            //app.UseElmahIo();

            ConfigureIdentity(context, userManager, roleManager);

            app.UseMvc();
        }

        // Criação de estruturas, usuários e permissões
        // na base do ASP.NET Identity Core (caso ainda não
        // existam)
        private static void ConfigureIdentity(OnCareContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) =>
            new IdentityInitializer(context, userManager, roleManager).Initialize();

        // Shows UseCors with CorsPolicyBuilder.
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