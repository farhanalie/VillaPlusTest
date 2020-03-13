using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using VillaPlus.API.Domain.Models.Exceptions;
using VillaPlus.API.Resources.Errors;

namespace VillaPlus.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new ErrorResponse
            {
                Error = new Error
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = exception.Message 
                }
            };

            #if DEBUG
                        error.Error.Details = new[]
                        {
                                new ErrorDetail
                                {
                                    Target = exception.TargetSite.ToString(),
                                    Message = exception.StackTrace
                                },
                                new ErrorDetail
                                {
                                    Target = exception.InnerException?.TargetSite.ToString(),
                                    Message = exception.InnerException?.Message
                                }
                            };
            #else
                            error.Error.Message = "Some Error Occured on server please try again";
            #endif

            switch (exception)
            {
                //case ForbiddenException _:
                //    error.Error.StatusCode = (int) HttpStatusCode.Forbidden;
                //    break;
                case BadRequestException _:
                    error.Error.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Error.StatusCode;
            return context.Response.WriteAsync(result);
        }
    }
}