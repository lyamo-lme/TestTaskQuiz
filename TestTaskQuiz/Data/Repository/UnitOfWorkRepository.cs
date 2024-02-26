using TestTaskQuiz.Core.Data;

namespace TestTaskQuiz.Data;

public class UnitOfWorkRepository : IUnitOfWorkRepository, IDisposable
{
    private readonly ILogger<UnitOfWorkRepository>? _logger;
    private readonly AppDbContext _forumDbContext;
    private readonly IRepositoryFactory _repositoryFactory;
    private Dictionary<string, object>? _repositories;

    public UnitOfWorkRepository(ILogger<UnitOfWorkRepository> logger, IRepositoryFactory repositoryFactory,
        AppDbContext forumDbContext)
    {
        _logger = logger;
        _repositoryFactory = repositoryFactory;
        _forumDbContext = forumDbContext;
    }

    public UnitOfWorkRepository(IRepositoryFactory repositoryFactory,
        AppDbContext forumDbContext)
    {
        _repositoryFactory = repositoryFactory;
        _forumDbContext = forumDbContext;
    }

    public void Dispose()
    {
        _forumDbContext.Dispose();
    }

    public IRepository<T> GenericRepository<T>() where T : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<string, object>();
        }

        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repository = _repositoryFactory.Instance<T>(_forumDbContext);
            _repositories.Add(type, repository);
        }

        return (IRepository<T>)_repositories[type];
    }

    public Task SaveAsync()
    {
        try
        {
            return _forumDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Critical, e, e.Message);
            throw;
        }
    }
}