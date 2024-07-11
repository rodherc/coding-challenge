namespace Coding.Challenge.Dependencies.Database
{
    public interface IMapper<TOut, in TIn>
    {
        TOut Map(Guid id, TIn item);
        TOut Patch(TOut oldItem, TIn newItem);

    }
}
