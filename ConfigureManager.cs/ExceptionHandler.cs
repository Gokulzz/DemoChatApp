using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using app.BLL.Exceptions;
using app.DAL.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace ConfigureManager.cs
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<User> _logger;
        public ExceptionHandler(RequestDelegate next, ILogger<User> logger)
        {
            _next = next;   
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionsAsync(HttpContext httpContext, Exception ex)
        {
            int statusCode;
            string message;
            var exception = ex.GetType();
            if(exception== typeof(NotFoundException))
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message= ex.Message;    
            }
            else if(exception== typeof(BadRequestException))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message= ex.Message;
            }
            else if(exception == typeof(UnauthorizedAccessException)) { 
                statusCode = (int)HttpStatusCode.Unauthorized;
                message= ex.Message;
            }
            else if(exception == typeof(NotImplementedException))
            {
                statusCode = (int)HttpStatusCode.NotImplemented;    
                message= ex.Message;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                message= ex.Message;    
            }
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            var exceptionResult = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = message
            });
            await httpContext.Response.WriteAsync(exceptionResult);
             _logger.LogError(exceptionResult.ToString());


        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
            return app;
        }
    }
}
