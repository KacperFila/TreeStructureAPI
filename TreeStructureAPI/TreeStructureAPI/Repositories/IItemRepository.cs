using TreeStructureAPI.Models;

namespace TreeStructureAPI.Repositories;

public interface IItemRepository
{
    public Task<Guid?> CreateItem(Item item);
    public Task<Item?> GetItemById(Guid id);
    public Task<List<Item>?> GetAllItems();
    public Task<List<Item>?> GetItemsExcludingParent(Guid id);
    public Task<bool> RenameItem(Guid id, Item item);
    public Task<bool> MoveItem(Guid id, Item item);
    public Task<bool> DeleteItem(Guid id);
}