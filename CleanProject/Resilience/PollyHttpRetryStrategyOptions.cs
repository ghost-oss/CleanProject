using System.Net;
using Polly;
using Polly.Retry;

namespace CleanProject.API.Resilience
{
    public static class PollyHttpRetryStrategyOptions
    {
        private static readonly List<HttpStatusCode> TransientStatusCodes = new()
        {
            HttpStatusCode.RequestTimeout,          // 408
            HttpStatusCode.InternalServerError,     // 500
            HttpStatusCode.BadGateway,              // 502
            HttpStatusCode.ServiceUnavailable,      // 503
            HttpStatusCode.GatewayTimeout           // 504
        };

        public static RetryStrategyOptions<HttpResponseMessage> GetHttpRetryOptions() =>
            new()
            {
                ShouldHandle = ShouldRetryAndHandleTransientError,
                MaxRetryAttempts = 3,
                BackoffType = DelayBackoffType.Exponential,
                Delay = TimeSpan.FromSeconds(2)
            };

        public static ValueTask<bool> ShouldRetryAndHandleTransientError(RetryPredicateArguments<HttpResponseMessage> args)
        {
            if(args.Outcome.Exception is HttpRequestException)
            {
                return PredicateResult.True();
            }

            if (args.Outcome.Result is HttpResponseMessage httpResponseMessage
                && TransientStatusCodes.Contains(httpResponseMessage.StatusCode))
            {
                return PredicateResult.True();
            }

            return PredicateResult.False();
        }
    }
}

