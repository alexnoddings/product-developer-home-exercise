using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data;

// Provides CRUD operations over the to-do repository
public interface ITodoListRepository
{
    /// <summary>
    ///     Gets a <see cref="TodoItem"/> by it's <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The <see cref="TodoItem.Id"/>.</param>
    /// <returns>A <see cref="TodoItem"/> if found; otherwise, <see langword="null"/>.</returns>
    public Task<TodoItem?> GetByIdAsync(Guid id);
    
    /// <summary>
    ///     Gets a list of all <see cref="TodoItem"/>s.
    /// </summary>
    public Task<List<TodoItem>> GetAllAsync();

    /// <summary>
    ///     Creates a new <paramref name="todo"/>.
    /// </summary>
    /// <param name="todo">The new to-do to create.</param>
    /// <returns>The created <see cref="TodoItem"/>'s id.</returns>
    public Task<Guid> CreateAsync(TodoItem todo);
    
    /// <summary>
    ///     Updates a <paramref name="todo"/>.
    /// </summary>
    /// <param name="todo">The to-do to update.</param>
    /// <returns><see langword="true"/> if the to-do item exists (and was updated); otherwise, <see langword="false"/>.</returns>
    public Task<bool> UpdateAsync(TodoItem todo);
    
    /// <summary>
    ///     Deletes a <see cref="TodoItem"/>.
    /// </summary>
    /// <param name="id">The <see cref="TodoItem.Id"/> of the <see cref="TodoItem"/>.</param>
    /// <returns><see langword="true"/> if the to-do item exists (and was deleted); otherwise, <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id);
}