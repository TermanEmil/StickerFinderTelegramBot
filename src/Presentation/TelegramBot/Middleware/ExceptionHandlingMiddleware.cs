using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TelegramBot.Middleware
{
    public class ExceptionHandlingMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken ct,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
