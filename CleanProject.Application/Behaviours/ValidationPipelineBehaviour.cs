using CleanProject.Domain.Validator;
using MediatR;

namespace CleanProject.Application.Behaviours
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: class
	{
        private ICommonValidator<TRequest> Validator { get; set; }

        public ValidationPipelineBehaviour(ICommonValidator<TRequest> validator)
        {
            this.Validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await Validator.ValidateAsync(request, true);

            var response = await next();

            return response;
        }
    }
}

