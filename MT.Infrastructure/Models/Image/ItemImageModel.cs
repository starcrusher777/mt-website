using System.Text.Json.Serialization;

namespace MT.Infrastructure.Models;

public class ItemImageModel : BaseModel
{
    public string ImageUrl { get; set; }

    public int ImageId { get; set; }
}