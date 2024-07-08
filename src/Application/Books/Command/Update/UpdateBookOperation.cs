using Application.Books.Exceptions;
using Application.Books.Validators.Books;
using Domain.Abstraction;
using Domain.Abstraction.Repositories;
using Domain.Core.Operation;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Application.Books.Command.Update;

public sealed class UpdateBookOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<UpdateBookRequest, UpdateBookResponse>> logger) 
    : CoreOperationAsync<UpdateBookRequest, UpdateBookResponse>(unitOfWork, logger), IUpdateBookOperation
{
    private Book _book = new();

    protected override async Task<UpdateBookResponse> ProcessOperationAsync(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        _book.Author = request.Author;
        _book.Title = request.Title;
        _book.RegisterNumber = request.RegisterNumber;

        var result = await _unitOfWork.GetRepository<IBookRepository>().UpdateAsync(_book);
        return (UpdateBookResponse)result;
    }

    protected override async Task<ValidationResult> ValidateAsync(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        _book = await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(request.Id);

        if (_book is null)
        {
            return new BookNotFoundException(request.Id);
        }

        var validator = new BookCreateValidator();
        return await validator.ValidateAsync((Book)request, cancellationToken);
    }
}
