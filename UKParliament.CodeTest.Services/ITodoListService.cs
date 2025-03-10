using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services;

// This interface assumes that to-dos can't be created as completed, nor can they be completed using the update function.
// Instead, they can only be completed using CompleteAsync, which is a one-way process.
public interface ITodoListService
{
    /// <summary>
    ///     Gets a <see cref="TodoModel"/> by it's <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The <see cref="TodoModel.Id"/>.</param>
    /// <returns>A <see cref="TodoModel"/> if found; otherwise, <see langword="null"/>.</returns>
    public Task<TodoModel?> GetByIdAsync(Guid id);
    
    /// <summary>
    ///     Gets a list of all <see cref="TodoModel"/>s.
    /// </summary>
    public Task<List<TodoModel>> GetAllAsync();
    
    /// <summary>
    ///     Creates a new to-do item using <paramref name="model"/>.
    /// </summary>
    /// <param name="model">The properties to create the to-do with.</param>
    /// <returns>The <see cref="TodoModel.Id"/> of the created to-do item.</returns>
    public Task<Guid> CreateAsync(CreateTodoModel model);
    
    /// <summary>
    ///     Updates a to-do item using <paramref name="model"/>.
    /// </summary>
    /// <param name="id">The <see cref="TodoModel.Id"/> of the to-do item.</param>
    /// <param name="model">The properties to update the to-do with.</param>
    /// <returns><see langword="true"/> if the to-do item exists (and was updated); otherwise, <see langword="false"/>.</returns>
    public Task<bool> UpdateAsync(Guid id, UpdateTodoModel model);
    
    /// <summary>
    ///     Completes a to-do item.
    /// </summary>
    /// <param name="id">The <see cref="TodoModel.Id"/> of the to-do item.</param>
    /// <returns><see langword="true"/> if the to-do item exists (and was completed); otherwise, <see langword="false"/>.</returns>
    /// <remarks>Returns <see langword="true"/> even if the to-do item is already completed.</remarks>
    public Task<bool> CompleteAsync(Guid id);
    
    /// <summary>
    ///     Deletes a to-do item.
    /// </summary>
    /// <param name="id">The <see cref="TodoModel.Id"/> of the to-do item.</param>
    /// <returns><see langword="true"/> if the to-do item exists (and was deleted); otherwise, <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
