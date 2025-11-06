using Library.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Library.Api.ExceptionFilters

{
    public class ApiExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<ApiExceptionFilter> ?_logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
            
        }

        public void OnException(ExceptionContext context)


        {

            switch (context.Exception)
            {
                case NotFoundException nf:
                    context.Result = new NotFoundObjectResult(new { error = nf.Message });
                    break;
                case ArgumentException arg:
                    context.Result = new BadRequestObjectResult(new { error = arg.Message });
                    break;
                default:
                    _logger.LogError(context.Exception, "Unhandled exception");
                    context.Result = new ObjectResult(new { error = "An unexpected error occurred." })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;

            }
            context.ExceptionHandled = true;

           
        }
    }
}
