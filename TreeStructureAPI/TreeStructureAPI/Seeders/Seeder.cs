using TreeStructureAPI.Models;

namespace TreeStructureAPI.Seeders;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Items.Any())
        {
            var items = new List<Item>()
            {
                new Item()
                {
                    Title = "Element 1",
                    ChildItems = new List<Item>()
                    {
                        new Item()
                        {
                            Title = "Element 1.1"
                        },
                        new Item()
                        {
                            Title = "Element 1.2"
                        },
                        new Item()
                        {
                            Title = "Element 1.3"
                        }
                    }
                },
                new Item()
                {
                    Title = "Element 2",
                    ChildItems = new List<Item>()
                    {
                        new Item()
                        {
                            Title = "Element 2.1"
                        },
                        new Item()
                        {
                            Title = "Element 2.2",
                            ChildItems = new List<Item>()
                            {
                                new Item()
                                {
                                    Title = "Element 2.2.1",
                                    ChildItems = new List<Item>()
                                    {
                                        new Item()
                                        {
                                            Title = "Element 2.2.1.1"
                                        },
                                        new Item()
                                        {
                                            Title = "Element 2.2.1.2"
                                        },
                                        new Item()
                                        {
                                            Title = "Element 2.2.1.3"
                                        }
                                    }
                                },
                            }
                        },
                        new Item()
                        {
                            Title = "Element 2.3"
                        }
                    }
                },
            };
            context.AddRange(items);
            context.SaveChanges();
        }
    }
}