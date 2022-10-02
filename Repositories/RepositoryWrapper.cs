
using System.Collections;

namespace Web.Repositories;
public interface IRepositoryWrapper
{
   IRepositoryBase<T> Repository<T>()
        where T : class;

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

public class RepositoryWrapper : IRepositoryWrapper
{
    private static readonly Hashtable Repositories = new();
    private readonly ApplicationDbContext applicationDbContext;

    public RepositoryWrapper(ApplicationDbContext applicationDbContext) => this.applicationDbContext = applicationDbContext;

    public IRepositoryBase<T> Repository<T>()
        where T : class
    {
        if (!Repositories.ContainsKey(typeof(T).FullName!))
        {
            Repositories.Add(typeof(T).FullName!, Activator.CreateInstance(typeof(RepositoryBase<>).MakeGenericType(typeof(T)), applicationDbContext));
        }

        return (IRepositoryBase<T>)Repositories[typeof(T).FullName!]!;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await applicationDbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) => await applicationDbContext.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => await applicationDbContext.Database.RollbackTransactionAsync(cancellationToken);
}
