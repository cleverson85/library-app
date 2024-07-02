using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IMongoDbContext context) : base(context)
    { }

    public async Task<User> GetByUserName(string userName, CancellationToken cancellationToken = default)
    {
        return await _collection.FindSync(c => c.UserName == userName.Trim()).FirstOrDefaultAsync(cancellationToken);
    }
}
