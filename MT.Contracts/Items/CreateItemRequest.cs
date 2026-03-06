namespace MT.Contracts.Items;

public class CreateItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}
