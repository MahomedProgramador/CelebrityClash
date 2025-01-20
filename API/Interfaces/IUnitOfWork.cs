namespace API.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository {get;}
    IMessageRepository MessageRepository {get;}
    IDislikesRepository DislikesRepository {get;}
    Task<bool> Complete();
    bool HasChanges();
}
