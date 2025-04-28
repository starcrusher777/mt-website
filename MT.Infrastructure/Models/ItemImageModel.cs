using System.Text.Json.Serialization;

namespace MT.Infrastructure.Models;

public class ItemImageModel : BaseModel
{
    public string ImageUrl { get; set; }   // Путь к файлу или URL (например, /images/item123.jpg или https://...)

    public int ImageId { get; set; }
}