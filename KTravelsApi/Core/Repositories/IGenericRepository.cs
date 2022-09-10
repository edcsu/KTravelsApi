namespace KTravelsApi.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Create an entity
    /// </summary>
    /// <param name="t">Entity is of type T</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> AddAsync(T t, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check whether an entity of type T exists 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync<TE>(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the total number of entities
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Find an entity of type T
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an entity of type T
    /// </summary>
    /// <param name="t"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T t, CancellationToken cancellationToken = default);
}