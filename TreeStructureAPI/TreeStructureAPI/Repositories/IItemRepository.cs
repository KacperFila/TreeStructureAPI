using TreeStructureAPI.Models;

namespace TreeStructureAPI.Repositories;

public interface IItemRepository
{
    public Task<Guid?> CreateItem(Item item);
    public Task<Item?> GetItemById(Guid id);
    public Task<List<Item>> GetAllItems(Guid? rootId, SortDirection? sortDirection);
    public Task<bool> UpdateItem(Guid id, Item item);
    public Task<bool> DeleteItem(Guid id);

}