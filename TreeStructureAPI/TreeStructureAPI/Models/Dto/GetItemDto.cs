namespace TreeStructureAPI.Models.Dto;

public class GetItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public List<GetItemDto> ChildItems { get; set; } = new List<GetItemDto>();
}