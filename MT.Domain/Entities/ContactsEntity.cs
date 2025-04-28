
namespace MT.Domain.Entities;

public class ContactsEntity: BaseEntity
{
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}