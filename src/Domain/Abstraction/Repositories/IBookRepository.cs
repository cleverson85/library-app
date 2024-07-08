using Domain.Core.Contract;
using Domain.Entities;

namespace Domain.Abstraction.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{ 
    Task<Book> GetBookByRegisterNumber(string registerNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBookByFilter(CoreOperationRequest requestFilter, CancellationToken cancellationToken = default);
}
