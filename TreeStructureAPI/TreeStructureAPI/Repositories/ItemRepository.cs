using Microsoft.EntityFrameworkCore;
using TreeStructureAPI.Models;

namespace TreeStructureAPI.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> CreateItem(Item item)
    {
        await _context.AddAsync(item);
        await _context.SaveChangesAsync();
        return await Task.FromResult(item.Id);
    }

    public async Task<Item?> GetItemById(Guid id)
    {
        var items = await _context.Items.ToListAsync();
        var item = items.SingleOrDefault(i => i.Id == id);
        return await Task.FromResult(item);
    }

    public async Task<List<Item>?> GetAllItems()
    {
        var items = await _context.Items.Include(i => i.ChildItems).ToListAsync();
        var roots = items.Where(i => i.ParentItemId == null).ToList();
        return await Task.FromResult(roots);
    }


    public async Task<bool> RenameItem(Guid id, Item item)
    {
        var itemToUpdate = await _context.Items.SingleOrDefaultAsync(i => i.Id == id);
        if (itemToUpdate is null) return await Task.FromResult(false);

        if (item.ParentItemId == id) return await Task.FromResult(false);
        
        itemToUpdate.Title = item.Title;

        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
    }
    
    public async Task<bool> MoveItem(Guid id, Item item)
    {
        var itemToUpdate = await _context.Items.SingleOrDefaultAsync(i => i.Id == id);
        if (itemToUpdate is null) return await Task.FromResult(false);

        if (item.ParentItemId == id) return await Task.FromResult(false);
        
        itemToUpdate.ParentItemId = item.ParentItemId;

        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
    }
    public async Task<List<Item>?> GetItemsExcludingParent(Guid id)
    {
        var allItems = await _context.Items.Include(i => i.ChildItems).ToListAsync();
        var excludedItemsList = new List<Item>();

        var givenItem = allItems.SingleOrDefault(i => i.Id == id);
        if (givenItem != null)
        {
            var parentItem = allItems.FirstOrDefault(i => i.ChildItems.Contains(givenItem));
            if (parentItem != null)
            {
                excludedItemsList.Add(parentItem);
                AddNestedItems(excludedItemsList, parentItem.ChildItems);
            }
        }

        var excludedItems = excludedItemsList.Concat(excludedItemsList.SelectMany(i => FlattenList(i.ChildItems))).ToList();
        var result = allItems.Except(excludedItems).ToList();

        return result.Select(i => new Item()
        {
            Id = i.Id,
            Title = i.Title
        }).ToList();
    }

    
    private void AddNestedItems(List<Item> result, List<Item> nestedItems)
    {
        foreach (var item in nestedItems)
        {
            result.Add(item);
            AddNestedItems(result, item.ChildItems);
        }
    }

    private List<Item> FlattenList(List<Item> items)
    {
        var flattenedItems = new List<Item>();
        foreach (var item in items)
        {
            flattenedItems.Add(item);
            flattenedItems.AddRange(FlattenList(item.ChildItems));
        }
        return flattenedItems;
    }

    public async Task<bool> DeleteItem(Guid id)
    {
        var item = await GetItemById(id);
        if (item != null)
        {
            await DeleteChildren(item);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    private async Task DeleteChildren(Item item)
    {
        if (item.ChildItems.Count > 0)
        {
            var childItems = item.ChildItems.ToList();
            foreach (var childItem in childItems)
            {
                await DeleteChildren(childItem);
                _context.Items.Remove(childItem);
            }
        }
        
        await _context.SaveChangesAsync();
        await Task.CompletedTask;
    }
}