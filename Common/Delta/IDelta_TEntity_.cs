namespace Common.Delta;

public interface IDelta<T> where T : class
{
    void Patch(T entity);
}