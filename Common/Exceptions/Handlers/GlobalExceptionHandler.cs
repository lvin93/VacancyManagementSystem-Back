using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Common.Models;
using FluentValidation;
using System.Net;
using System.ComponentModel;

namespace Common.Exceptions.Handlers
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, exception.Message);
            (string Details, string Title, int? StatusCode) = exception switch
            {
                BaseException baseEx =>
                (exception.StackTrace, exception.Message, baseEx.StatusCode),
                ValidationException validationEx =>
                (exception.StackTrace, string.Join(" ", validationEx.Errors.Select(f => f.ErrorMessage)), StatusCodes.Status400BadRequest),
                _ =>
                (exception.StackTrace, exception.Message, StatusCodes.Status500InternalServerError)
            };

            DomainError domainError = new DomainError { ErrorMessage = Title, InnerException = Details, StatusCode = StatusCode };


            await httpContext.Response.WriteAsJsonAsync(domainError);
            return true;
        }
    }
}
