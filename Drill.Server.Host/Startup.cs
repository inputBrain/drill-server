using Drill.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Drill.Server.Host;

public class Startup
{
    private ILogger _logger;
    private readonly ILoggerFactory _loggerFactory;
    public IConfiguration Configuration { get; }
    private IWebHostEnvironment CurrentEnvironment{ get; set; }

    
    public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment, ILoggerFactory loggerFactory)
    {
        Configuration = configuration;
        _logger = loggerFactory.CreateLogger<Startup>();
        CurrentEnvironment = currentEnvironment;
        _loggerFactory = loggerFactory;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors
        (
            options =>
            {
                options.AddPolicy
                (
                    "CorsPolicy",
                    policy =>
                        policy.WithOrigins(Configuration.GetSection("AllowedHosts").Value!)
                            .WithMethods("POST", "GET", "DELETE", "OPTIONS")
                            .WithHeaders("*")
                );
                options.AddPolicy
                (
                    "apiDocumentation",
                    policy =>
                        policy.WithOrigins(
                                "http://localhost:3000",
                                "http://164.92.199.150:3500",
                                "https://164.92.199.150:3500"
                            )
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                );
            }
        );
        // services.Configure<ImageConfig>(Configuration.GetSection("Image"));
        


        Type typeOfContent = typeof(Startup);
        services.AddDbContext<PostgreSqlContext>
        (
            opt => opt.UseNpgsql
            (
                Configuration.GetConnectionString("PostgreSqlConnection"),
                b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
            )
        );

        services.AddScoped<IDatabaseContainer, DatabaseContainer>();
        //
        // services.AddScoped<IServiceContainer>(sp => 
        //     Factory.Create(
        //         _loggerFactory,
        //         sp.GetRequiredService<IDatabaseContainer>()
        //     )
        // );


        // services.AddScoped(
        //     sp => Core.UseCase.Factory.Create(
        //         _loggerFactory,
        //         sp.GetRequiredService<IDatabaseContainer>(),
        //         sp.GetRequiredService<IServiceContainer>()
        //     )
        // );

        services.AddControllers();
        
        services.AddSwaggerGen(
            c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Drill.Host", Version = "v1"}); }
        );

    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Drill.Host v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("apiDocumentation");

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}