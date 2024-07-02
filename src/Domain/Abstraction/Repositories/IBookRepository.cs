using Domain.Entities;

namespace Domain.Abstraction.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{ 
    Task<Book> GetBookByRegisterNumber(string registerNumber, CancellationToken cancellationToken = default);
    Task<List<Book>> GetBookByFilter(string author, string title, string registerNumber, CancellationToken cancellationToken = default);
}
