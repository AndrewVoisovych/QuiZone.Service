using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuiZone.Common.LoggerService;
using System;
using System.Data;
using System.Threading.Tasks;

namespace QuiZone.API.Filters
{
    public class DeleteExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        private readonly ILoggerManager logger;
        public DeleteExceptionFilterAttribute(ILoggerManager logger)
        {
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var result = new ContentResult();

            result.Content = context.Exception.Message;
            result.StatusCode =
                exceptionType == typeof(UnauthorizedAccessException)
                    ? StatusCodes.Status403Forbidden
                    : exceptionType == typeof(ConstraintException)
                        ? StatusCodes.Status409Conflict
                        : StatusCodes.Status400BadRequest;

            context.Result = result;
            context.ExceptionHandled = true;
            logger.Error($"DeleteException. StatusCode: {result.StatusCode}, Message: {result.Content} ");

            return Task.CompletedTask;
        }
    }
}
