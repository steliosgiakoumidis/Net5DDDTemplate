using System;
using System.Threading.Tasks;
using Example.Business.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(DatabaseLayerException))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync($"An error occured on the database level. Error: {e.InnerException.Message}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(GetProblemDetails(context)));
                    await context.Response.WriteAsync($"Internal server Error. Exception: {e}");
                    
                }
            }   
            
            ValidationProblemDetails GetProblemDetails(HttpContext context)
            {
                var validationProblemDetails = new ValidationProblemDetails
                {
                    Status = 500, 
                    Title = "One or more errors occured"
                };
            
                validationProblemDetails.Errors.Add("message", new[] {"Internal server error"});
                validationProblemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        
                return validationProblemDetails;
            }

        }
    }
}