namespace MT.Infrastructure.Models;

public class ItemModel : BaseModel
{
    public long ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    
    public ICollection<ItemImageModel> Images { get; set; }
}