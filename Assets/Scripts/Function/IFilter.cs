namespace Function
{
    public interface IFilter<T>
    {
        public bool Filter(T data);
    }
}
