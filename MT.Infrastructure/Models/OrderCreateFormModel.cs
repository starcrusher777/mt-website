using System.ComponentModel.DataAnnotations;
using MT.Domain.Enums;

namespace MT.Infrastructure.Models;

public class OrderCreateFormModel : BaseModel
{
    public string OrderName { get; set; }
    public OrderStatus Status { get; set; }
    public OrderType Type { get; set; }

    public ItemCreateFormModel Item { get; set; }

    public IFormFile[]? Images { get; set; } // новые изображения
}