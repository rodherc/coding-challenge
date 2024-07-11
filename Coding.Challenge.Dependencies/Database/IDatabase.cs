namespace Coding.Challenge.Dependencies.Database
{
    public interface IDatabase<TOut, in TIn>
    {
        Task<TOut?> Create(TIn item);
        Task<TOut?> Read(Guid id);
        Task<IEnumerable<TOut?>> ReadAll();
        Task<TOut?> Update(Guid id, TIn item);
        Task<Guid> Delete(Guid id);
    }
}
