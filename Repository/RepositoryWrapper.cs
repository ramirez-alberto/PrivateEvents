using PrivateEvents.Entities;
using PrivateEvents.Contracts;

namespace PrivateEvents.Repository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private RepositoryContext _repositoryContext;
    private IEventRepository _event;

    public IEventRepository Event 
    {
        get
        {
            return _event ?? new EventRepository(_repositoryContext);
        }
    }

    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    public async Task SaveAsync() =>
        await _repositoryContext.SaveChangesAsync();
}