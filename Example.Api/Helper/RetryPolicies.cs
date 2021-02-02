using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Example.Api.Helpers
{
    public static class RetryPolicies
    {

        public static IAsyncPolicy<HttpResponseMessage> GetHttpPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4)
                }, (exception, timeSpan, retryCount, context) => {
                    Log.Error($"Retry attempt: {retryCount}");    
                }); 
        }
    }
}