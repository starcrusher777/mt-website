namespace MT.Contracts.Orders;

public class OrderResponse
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public string OrderName { get; set; } = string.Empty;
    public OrderStatusDto Status { get; set; }
    public OrderTypeDto Type { get; set; }
    public long? UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public OrderItemDto Item { get; set; } = new();
}

public class OrderItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    public List<OrderItemImageDto> Images { get; set; } = new();
}

public class OrderItemImageDto
{
    public long Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
