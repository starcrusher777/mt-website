namespace MT.Domain.Interfaces;

public interface IFileRepository
{
    Task DeleteFileAsync(string imageUrl);
}