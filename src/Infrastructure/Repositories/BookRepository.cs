using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(IMongoDbContext context) : base(context)
    { }

    public async Task<Book> GetBookByRegisterNumber(string registerNumber, CancellationToken cancellationToken = default)
    {
        return await _collection.FindSync(c => c.RegisterNumber == registerNumber.Trim())
                                .FirstOrDefaultAsync();
    }
}
