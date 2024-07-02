using Application.Books.Queries.Filter;
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

    public async Task<List<Book>> GetBookByFilter(string author, string title, string registerNumber, CancellationToken cancellationToken = default)
    {
        List<Book> result = new();

        if (!string.IsNullOrWhiteSpace(author) &&
            string.IsNullOrWhiteSpace(title) &&
            string.IsNullOrWhiteSpace(registerNumber))
        {
            result = _collection.AsQueryable()
                                .Where(c => c.Author == author).ToList();
        }
        else if (!string.IsNullOrWhiteSpace(title) &&
                  string.IsNullOrWhiteSpace(author) &&
                  string.IsNullOrWhiteSpace(registerNumber))
        {
            result = _collection.AsQueryable()
                                .Where(c => c.Title == title).ToList();
        }
        else if (!string.IsNullOrWhiteSpace(registerNumber) &&
                  string.IsNullOrWhiteSpace(author) &&
                  string.IsNullOrWhiteSpace(title))
        {
            result = _collection.AsQueryable()
                                .Where(c => c.RegisterNumber == registerNumber).ToList();
        }
        else
        {
            result = _collection.AsQueryable()
                                .Where(c => (c.Title == title) &&
                                            (c.Author == author) &&
                                            (c.RegisterNumber == registerNumber)).ToList();
        }

        

        return await Task.FromResult(result);
    }
}
