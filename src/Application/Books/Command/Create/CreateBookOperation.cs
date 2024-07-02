using Application.Books.Exceptions;
using Application.Books.Validators.Books;
using Application.Core.Contract;
using Application.Core.Operation;
using Domain.Abstraction;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Application.Books.Command.Create;

public sealed class CreateBookOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<CreateBookRequest, CreateBookResponse>> logger) 
    : CoreOperationAsync<CreateBookRequest, CreateBookResponse>(unitOfWork, logger), ICreateBookOperation
{
    protected override async Task<CreateBookResponse> ProcessOperationAsync(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.BookRepository.SaveAsync((Book)request, cancellationToken);
        return (CreateBookResponse)result;
    }

    protected override async Task<ValidationResult> ValidateAsync(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var result = await IsRegisterNumberAlreadyExists(request.RegisterNumber, cancellationToken);

        if (!result.IsValid)
        {
            return result;
        }

        var validator = new BookCreateValidator();
        return await validator.ValidateAsync((Book)request, cancellationToken);
    }

    private async Task<ValidationResult> IsRegisterNumberAlreadyExists(string registerNumber, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.GetBookByRegisterNumber(registerNumber, cancellationToken);
        if (book is not null)
        {
            return new BookAlreadyExistisdException(book.RegisterNumber);
        }

        return new CoreOperationResponse();
    }
}
