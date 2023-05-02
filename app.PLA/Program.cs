using app.PLA;
using ConfigureManager.cs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
try
{
    var builder = WebApplication.CreateBuilder(args);
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithThreadName()
        .Enrich.WithThreadId()
        .Enrich.WithProcessId()
        .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    // Add services to the container.

    builder.Services.AddControllers().AddNewtonsoftJson();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ConfigureServices();
    builder.Services.AddSignalR();
    builder.Services.AddScoped<ChatHub>();
    builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("V1", new OpenApiInfo
        {
            Version = "V1",
            Title = "WebAPI",
            Description = "ChatApp WebAPI"
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List <string>()
        }
    });
    });
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetSection("JWT").GetSection("ValidIssuer").Value,
                ValidAudience = builder.Configuration.GetSection("JWT").GetSection("ValidAudience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")
                .GetSection("SecretKey").Value))


            }; 

        });
    builder.Services.AddSwaggerGenNewtonsoftSupport();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/V1/swagger.json", "ChatApp WebAPI");
        });
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseExceptionMiddleware();
    app.MapControllers();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<ChatHub>("/chat");
    });
   

    app.Run();
    
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());   
}
finally
{
    Log.CloseAndFlush();
}
