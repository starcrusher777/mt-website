namespace MT.Contracts.Items;

public class ItemResponse
{
    public long Id { get; set; }
    public long ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public List<ItemImageDto> Images { get; set; } = new();
}
