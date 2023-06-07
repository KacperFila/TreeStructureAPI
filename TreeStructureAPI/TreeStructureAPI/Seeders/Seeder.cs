using TreeStructureAPI.Models;

namespace TreeStructureAPI.Seeders;

public static class Seeder
{
    public static void Seed(AppDbContext _context)
    {
        var items = new List<Item>()
        {
            new Item()
            {
                Title = "Honda",
                ChildItems = new List<Item>()
                {
                    new Item()
                    {
                        Title = "Civic",
                        ChildItems = new List<Item>()
                        {
                            new Item()
                            {
                                Title = "VI"
                            },
                            new Item()
                            {
                                Title = "VII"
                            },
                            new Item()
                            {
                                Title = "VIII",
                                ChildItems = new List<Item>()
                                {
                                    new Item()
                                    {
                                        Title = "Sedan"
                                    },
                                    new Item()
                                    {
                                        Title = "Hatchback"
                                    },
                                    new Item()
                                    {
                                        Title = "Coupe"
                                    },
                                }
                            },
                        }
                    },
                    new Item()
                    {
                        Title = "Integra"
                    },
                    new Item()
                    {
                        Title = "NSX"
                    }
                }
            },
            new Item()
            {
                Title = "Peugeot",
                ChildItems = new List<Item>()
                {
                    new Item()
                    {
                        Title = "RCZ"
                    },
                    new Item()
                    {
                        Title = "508"
                    },
                    new Item()
                    {
                        Title = "407"
                    }
                }
            },

        };

        if (!_context.Items.Any())
        {
            _context.AddRange(items);
            _context.SaveChanges();
        }
    }
}