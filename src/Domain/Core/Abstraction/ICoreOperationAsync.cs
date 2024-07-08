using Domain.Core.Contract;
using FluentValidation.Results;

namespace Domain.Core.Abstraction;

public interface ICoreOperationAsync<TRequest, TResponse> : IDisposable
    where TRequest : CoreOperationRequest
    where TResponse : CoreOperationResponse, new()
{
    Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken = default);
    protected Task<ValidationResult> ValidateOperationAsync(TRequest request, CancellationToken cancellationToken = default);
}

