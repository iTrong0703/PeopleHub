using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Domain.Common;
using PeopleHub.Infrastructure.Data;

namespace PeopleHub.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private bool _disposed;

        public IUserRepository Users { get; }

        public UnitOfWork(
            AppDbContext context,
            ILogger<UnitOfWork> logger,
            IUserRepository userRepository
            )
        {
            _context = context;
            _logger = logger;
            Users = userRepository;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = _context.ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTimeOffset.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModified = DateTimeOffset.UtcNow;
                }
            }
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during SaveChangesAsync");
                throw;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                _disposed = true;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }

    }
}
