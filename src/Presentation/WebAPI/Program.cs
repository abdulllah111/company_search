using System.Reflection;
using Application;
using Application.Common.Interfaces;
using Persistence;
// using WebAPI.Middleware;
using Application.Common.Mappings;
using WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

using (var scope = builder.Services?.BuildServiceProvider().CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        ApplicationDbContextInitializer.Initialize(context);
    }
    catch (Exception ex)
    {

    }
}

var app = builder.Build();

Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    
    services.AddAutoMapper(config =>
    {
        config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new MappingProfile(typeof(IApplicationDbContext).Assembly));
    });
    services.AddApplicationServices();
    services.AddPersistenceServices(builder.Configuration);
    services.AddControllers();
    services.AddCors(option =>
    {
        option.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

    services.AddAuthentication(config => {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        options.Authority = "https://localhost:44352/";
        options.Audience = "CompanySearchWebAPI";
        options.RequireHttpsMetadata = false;
    });

    services.AddSwaggerGen(config =>{
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        config.IncludeXmlComments(xmlPath);
    });
}

void Configure(WebApplication app)
{

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }


    // exceptHundler -> hsts -> httpsredirection -> files -> cookie -> routin -> cors -> authentication - authorization -> sess -> mvc
    app.UseSwagger();
    app.UseSwaggerUI(config =>{
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "ConpanySearch Web API");
    });
    app.UseCustomExceptionHandler();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}
