using canchasfutbol.Application.Dtos;
using FluentValidation;
using MediatR;
namespace canchasfutbol.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList(); 

                if(failures.Any())
                {
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList(); 

                    // Obtener el tipo real de T en ResponseDto<T>

                    var responseType = typeof(TResponse);

                    if(responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(ResponseDto<>))
                    {
                       var innerType = responseType.GetGenericArguments()[0];
                        // Crear una instancia de ResponseDto<T> con los mensajes de error
                       var method = typeof(ResponseDto<>)
                            .MakeGenericType(innerType)
                            .GetMethod(nameof(ResponseDto<object>.Failure))!;
                        
                        var responseDto = method.Invoke(null, new object[] { errorMessages, "Validation errors occurred" });

                        return (TResponse)responseDto!;

                    }
                    throw new InvalidCastException("El tipo de respuesta esperado deber ser ResponseDto<T>");
                }
            }

            return await next();
        }
    }
}
