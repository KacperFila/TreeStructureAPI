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

    public async Task<bool> UpdateItem(Guid id, Item item)
    {
        var itemToUpdate = await _context.Items.SingleOrDefaultAsync(i => i.Id == id);
        if (itemToUpdate is null) return await Task.FromResult(false);

        if (item.ParentItemId == id) return await Task.FromResult(false);
        
        itemToUpdate.Title = item.Title;
        itemToUpdate.ChildItems = item.ChildItems;
        itemToUpdate.ParentItemId = item.ParentItemId;
        
        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
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