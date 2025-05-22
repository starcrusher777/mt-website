using MT.Domain.Enums;

namespace MT.Infrastructure.Models;

public class OrderUpdateModel : BaseModel
{
    public string OrderName { get; set; }
    public OrderStatus Status { get; set; }
    public OrderType Type { get; set; }
    public long? UserId { get; set; }

    public ItemUpdateModel Item { get; set; }

    public IFormFile[]? Images { get; set; }
    
    public long[]? ImagesToDelete { get; set; }
}