using TreeStructureAPI.Models;
using TreeStructureAPI.Models.Dto;

namespace TreeStructureAPI.Services;

public interface IItemService
{
    public Task<Guid?> CreateItem(Item item);
    public Task<Item?> GetItemById(Guid id);
    public Task<List<Item>?> GetAllItems(Guid? rootId, SortDirection? sortDirection);
    public Task<bool> UpdateItem(Guid id, Item item);
    public Task<bool> DeleteItem(Guid id);
}