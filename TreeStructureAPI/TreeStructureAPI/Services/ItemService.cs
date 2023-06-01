using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreeStructureAPI.Models;
using TreeStructureAPI.Models.Dto;
using TreeStructureAPI.Repositories;

namespace TreeStructureAPI.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<Guid?> CreateItem(Item item)
    {
        var result = await _itemRepository.CreateItem(item);
        return await Task.FromResult(result);
    }

    public async Task<Item?> GetItemById(Guid id)
    {
        var result = await _itemRepository.GetItemById(id);
        return await Task.FromResult(result);
    }

    public async Task<List<Item>?> GetAllItems(Guid? rootId, SortDirection? sortDirection)
    {
        var items = await _itemRepository.GetAllItems(rootId, sortDirection);
        //var dtos = items.Where(e => e.ParentItemId == null).Select(MapItem).ToList();
        return await Task.FromResult(items);
    }

    public async Task<bool> UpdateItem(Guid id, Item item)
    {
        var result = await _itemRepository.UpdateItem(id, item);
        return await Task.FromResult(result);
    }

    public async Task<bool> DeleteItem(Guid id)
    {
        var result = await _itemRepository.DeleteItem(id);
        return await Task.FromResult(result);
    }

    private GetItemDto MapItem(Item item)
    {
        var itemDto = _mapper.Map<GetItemDto>(item);
        itemDto.ChildItems = item.ChildItems.Select(MapItem).ToList();
        return itemDto;
    }
}