using Application.Books.Queries.Filter;
using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using Domain.Core.Contract;
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

    public async Task<IEnumerable<Book>> GetBookByFilter(CoreOperationRequest requestFilter, CancellationToken cancellationToken = default)
    {
        var request = (BookRequestFilter)requestFilter;
        var builder = Builders<Book>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrWhiteSpace(request.Author))
        {
            filter &= builder.Eq(c => c.Author, request.Author);
        }

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            filter &= builder.Eq(c => c.Title, request.Title);
        }

        if (!string.IsNullOrWhiteSpace(request.RegisterNumber))
        {
            filter &= builder.Eq(c => c.RegisterNumber, request.RegisterNumber);
        }


        if (filter == builder.Empty)
        {
            return Enumerable.Empty<Book>();    
        }

        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }
}
