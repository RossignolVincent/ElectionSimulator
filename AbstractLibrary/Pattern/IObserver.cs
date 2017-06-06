namespace AbstractLibrary.Pattern
{
    public interface IObserver<T>
    {
        void Update(T obj);
    }
}