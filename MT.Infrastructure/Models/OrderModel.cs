using MT.Domain.Entities;
using MT.Domain.Enums;

namespace MT.Infrastructure.Models;

public class OrderModel : BaseModel
{
    public long OrderId { get; set; }
    public string OrderName { get; set; }
    public OrderStatus Status { get; set; }
    public OrderType OrderType { get; set; }
    
    public ItemModel Item { get; set; }
}