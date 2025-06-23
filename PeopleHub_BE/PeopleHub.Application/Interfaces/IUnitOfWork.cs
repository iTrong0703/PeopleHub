using PeopleHub.Application.Interfaces.Repositories;

namespace PeopleHub.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users  { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
