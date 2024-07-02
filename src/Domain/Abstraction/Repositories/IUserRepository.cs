using Domain.Entities;

namespace Domain.Abstraction.Repositories;

public interface IUserRepository : IBaseRepository<User>
{ 
    Task<User> GetByUserName(string userNamem, CancellationToken cancellationToken = default);
}
