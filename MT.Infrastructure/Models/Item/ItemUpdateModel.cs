namespace MT.Infrastructure.Models;

public class ItemUpdateModel : BaseModel
{
    public string Description { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}