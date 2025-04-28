namespace MT.Domain.Entities;

public class ItemEntity: BaseEntity
{
    public long ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    
    public virtual ICollection<ItemImageEntity> Images { get; set; } = new List<ItemImageEntity>();
}