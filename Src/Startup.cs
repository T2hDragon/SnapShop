using Microsoft.EntityFrameworkCore;
using SnapShop.Data;
using Microsoft.OpenApi.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SnapShop.Framework.Authentication;

public class Startup
{
    private IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddDataAnnotationsLocalization();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            });
        services.AddRouting(options => options.LowercaseUrls = true);

        DependencyInjection.Add(services);
        SnapShop.Framework.Authentication.Jwt.AddToService(services);

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            string defaultCulture = "en-GB";
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(defaultCulture),
                new CultureInfo("et-EE")
            };

            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerGeneratorOptions.ConflictingActionsResolver = (apiDescriptions) => apiDescriptions.First();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Application API",
                Description = "Application Documentation",
                Contact = new OpenApiContact { Name = "Author" },
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://en.wikipedia.org/wiki/MIT_License") }
            });
            Jwt.AddToSwagger(options);
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {       
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

