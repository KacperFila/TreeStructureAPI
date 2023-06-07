namespace TreeStructureAPI.Models;

public class Item
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public Guid? ParentItemId { get; set; }
    public Item ParentItem { get; set; }= default!;
    public List<Item> ChildItems { get; set; } = new List<Item>();
}