using Core.Exceptions.Base;
using System.Net;
using System.Text.Json;

namespace Catalog.API.Middleware
{
    public class Response
    {
        public int Severity { get; set; }
        public int StatusCode { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpTransactionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpTransactionHandlerMiddleware> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public HttpTransactionHandlerMiddleware(RequestDelegate next, ILogger<HttpTransactionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //Autenticacion
            try
            {
                //TODO: implement global validator
                //_validator.Validate();
                await HandleValidation(context).ConfigureAwait(false);
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e).ConfigureAwait(false);
            }
        }
        /*
         * ojota con esto eee
         
         public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // Invoke the validators
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .ToArray();

        if (failures.Length > 0)
        {
            // Map the validation failures and throw an error,
            // this stops the execution of the request
            var errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage).ToArray());
            throw new InputValidationException(errors);
        }

        // Invoke the next handler
        // (can be another pipeline behavior or the request handler)
        return next();
    }
}
         */

        private async Task HandleValidation(HttpContext context)
        {
            //TODO: IValidator resolver from url

            //TODO: invoke validation

        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            // search for static
            context.Response.ContentType = "application/problem+json";

            var result = JsonSerializer.Serialize(new Response
            {
                StatusCode = exception switch
                {
                    CatalogApiBaseException => StatusCodes.Status401Unauthorized,
                    DatabaseException => status.database
                    _ => StatusCodes.Status500InternalServerError,
                }
            });

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            _logger.LogError(result);
            await context.Response.WriteAsync(result);
        }
    }
}
