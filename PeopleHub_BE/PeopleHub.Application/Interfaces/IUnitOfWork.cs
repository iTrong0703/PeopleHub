using PeopleHub.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleHub.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users  { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
