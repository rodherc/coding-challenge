namespace Coding.Challenge.Dependencies.Database
{
    public interface IDadosMockados<TOut>
    {
        IDictionary<Guid, TOut> GerarMocks();
    }
}
