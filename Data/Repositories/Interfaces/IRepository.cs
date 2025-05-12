namespace PROG7311_POE.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves all entities from the data source
        Task<IEnumerable<T>> GetAllAsync();

        // Retrieves a single entity by its unique ID
        Task<T> GetByIdAsync(int id);

        // Adds a new entity to the data source
        Task AddAsync(T entity);

        // Updates an existing entity in the data source
        Task UpdateAsync(T entity);

        // Removes an existing entity from the data source
        Task DeleteAsync(T entity);

        // Save all pending changes to the data source
        Task SaveChangesAsync();

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
