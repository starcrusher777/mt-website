using MT.Domain.Interfaces;

namespace MT.Infrastructure.Data.Repositories;

public class FileRepository(IWebHostEnvironment env) : IFileRepository
{
    public Task DeleteFileAsync(string relativePath)
    {
        var fullPath = Path.Combine(env.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

        return Task.Run(() =>
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        });
    }
}