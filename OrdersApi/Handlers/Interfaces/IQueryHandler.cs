namespace OrdersApi.Handlers.Interfaces
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }

    public interface IQueryHandler<TResult>
    {
        Task<TResult> HandleAsync(CancellationToken cancellationToken = default);
    }
}
