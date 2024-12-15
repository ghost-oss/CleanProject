using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CleanProject.Domain.Filters
{
    public class TestFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        public TestFilter(ILogger<TestFilter> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogWarning("ACTION EXECUTED");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}

