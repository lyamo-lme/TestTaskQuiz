namespace TestTaskQuiz.Core.Data;

public interface IUnitOfWorkRepository:IDisposable
{
    public IRepository<T> GenericRepository<T>() where T : class;
    Task SaveAsync();
}