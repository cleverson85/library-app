using Domain.Entities;

namespace Domain.Abstraction.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{ 
    Task<Book> GetBookByRegisterNumber(string registerNumber, CancellationToken cancellationToken = default);
}
