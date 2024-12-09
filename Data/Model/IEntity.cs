namespace MVVMCrud.Data.Model
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}
