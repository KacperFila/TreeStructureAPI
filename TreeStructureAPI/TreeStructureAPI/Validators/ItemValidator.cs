using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TreeStructureAPI.Models;

namespace TreeStructureAPI.Validators;

public class ItemValidator : AbstractValidator<Item>
{
    private readonly AppDbContext _context;
    public ItemValidator(AppDbContext context)
    {
        _context = context;
        RuleFor(i => i.Title).Length(1, 20).WithMessage("Tytuł powinien zawierać się między 1 a 20 znakami!");
        RuleFor(i => i.Title).Must(NotExistInParent)
            .WithMessage("Na danym poziomie istnieje już item o tej nazwie!");
    }

    private bool NotExistInParent(Item item, string title)
    {
        var parentEntity = _context.Items
            .Include(i => i.ChildItems)
            .FirstOrDefault(parent => parent.Id == item.ParentItemId);

        if (parentEntity != null)
        {
            return !parentEntity.ChildItems.Any(child => child.Title == title);
        }

        return !_context.Items.Any(i => i.Title == title);
    }
}