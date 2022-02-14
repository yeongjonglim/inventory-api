using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using InventoryAPI.Application;
using InventoryAPI.Application.Common.Interfaces;
using InventoryAPI.Infrastructure;
using InventoryAPI.Infrastructure.Persistence;
using InventoryAPI.WebAPI.Filters;
using InventoryAPI.WebAPI.Services;

namespace InventoryAPI.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        // services.AddControllersWithViews(options =>
        //         options.Filters.Add<ApiExceptionFilterAttribute>())
        //     .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventoryAPI", Version = "v1" });
            c.OperationFilter<CustomAuthenticationFilter>();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryAPI v1"));
        
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHealthChecks("/health");
        // app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseCors("AllowAll");
        
        app.UseEndpoints(endpoints=>
        {
            endpoints.MapControllers();
        });
    }
}