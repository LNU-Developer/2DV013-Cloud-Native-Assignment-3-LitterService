using System.Diagnostics;
using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using LitterService.Application.Exceptions;

namespace LitterService.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IHostEnvironment environment)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex, environment);
            }
        }

        private static Task ConvertException(HttpContext context, Exception ex, IHostEnvironment environment)
        {
            var httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = string.Empty;
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            switch (ex)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.ValidationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = $"{badRequestException.Message} traceId: {traceId}";
                    break;
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case { } exception:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;

            }

            context.Response.StatusCode = (int)httpStatusCode;
            if (result == string.Empty && environment.IsDevelopment())
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }
            else if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new
                {
                    error = $"An error occured with statuscode: {context.Response.StatusCode} and traceId: {traceId}"
                });
            }
            return context.Response.WriteAsync(result);
        }
    }
}