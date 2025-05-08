using System.ComponentModel.DataAnnotations.Schema;
using MT.Domain.Enums;

namespace MT.Domain.Entities;

public class OrderEntity: BaseEntity
{
    public long OrderId { get; set; }
    public string OrderName { get; set; }
    public OrderStatus Status { get; set; }
    public OrderType Type { get; set; }
    
    public virtual ItemEntity Item { get; set; } = new ItemEntity();
    
    public long? UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }
}