﻿using Domain.Core.Contract;
using Domain.Entities;

namespace Application.Books.Command.Update;

public sealed class UpdateBookRequest : CoreOperationRequest
{
    public UpdateBookRequest(string id, string author, string title, string registerNumber)
    {
        Id = id;
        Author = author;
        Title = title;
        RegisterNumber = registerNumber;
    }

    public string Id { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string RegisterNumber { get; set; } = string.Empty;

    public static explicit operator Book(UpdateBookRequest bookRequest) => new()
    {
        Author = bookRequest.Author,
        Title = bookRequest.Title,
        RegisterNumber = bookRequest.RegisterNumber,
    };
}
