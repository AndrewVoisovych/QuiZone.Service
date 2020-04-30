using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QuiZone.Common.GlobalErrorHandling;
using QuiZone.Common.GlobalErrorHandling.Models;
using QuiZone.Common.LoggerService;
using System;
using System.Net;
using System.Threading.Tasks;

namespace QuiZone.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerManager logger;


        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            this.logger = logger;
            this.next = next;
        }

        /// <summary>
        ///  The invoke.
        /// </summary>
        /// <param name="httpContext">The context</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    logger.Warn(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Hanlding Exception info
        /// </summary>
        /// <param name="context">error context</param>
        /// <param name="exception">object exception</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is HttpException httpException)
            {
                context.Response.StatusCode = (int)httpException.StatusCode;
                context.Response.ContentType = httpException.ContentType;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = @"application/json";
                logger.Error($"0, {exception}, An unhandled exception has occurred: {exception.Message}");
            }

            var errorBody = JsonConvert.SerializeObject(new ErrorDetails(exception.Message));
            logger.Error(errorBody);

            return context.Response.WriteAsync(errorBody);
        }
    }
}
