using TreeStructureAPI.Models;
using TreeStructureAPI.Models.Dto;

namespace TreeStructureAPI.Services;

public interface IItemService
{
    public Task<Guid?> CreateItem(Item item);
    public Task<Item?> GetItemById(Guid id);
    public Task<List<GetItemDto>?> GetAllItems();
    public Task<List<GetItemDto>?> GetItemsExcludingParent(Guid id);
    public Task<bool> RenameItem(Guid id, Item item);
    public Task<bool> MoveItem(Guid id, Item item);
    public Task<bool> DeleteItem(Guid id);
}