using TestTaskQuiz.Core.Data;

namespace TestTaskQuiz.Data;

public class RepositoryFactory : IRepositoryFactory
{
    public IRepository<T> Instance<T>(object dbContext) where T : class
    {
        var definitionStack = new Stack<Type>();
        var type = typeof(GenericRepository<>);
        while (!type.IsGenericTypeDefinition)
        {
            definitionStack.Push(type.GetGenericTypeDefinition());
            type = type.GetGenericArguments()[0];
        }
        type = type.MakeGenericType(typeof(T));
        while (definitionStack.Count > 0)
            type = definitionStack.Pop().MakeGenericType(type);
        var inst = Activator.CreateInstance(type, dbContext);
        return (IRepository<T>)inst;
      
    }
}