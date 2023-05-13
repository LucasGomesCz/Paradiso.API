using System.Security.Cryptography;

namespace Paradiso.API.Service.Utils;

public static class Blob
{
    public static string GetHashCodeFromFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new ExceptionDto() { Message = EException.FileNotSelected.DisplayName() };

        using var sha256 = SHA256.Create();
        using var stream = file.OpenReadStream();

        var hashBytes = sha256.ComputeHash(stream);
        var hash = BitConverter.ToString(hashBytes).Replace("-", "");

        return hash;
    }

    public static async Task<string> UploadBlobFileAsync(BlobContainerClient containerClient, IFormFile file, Guid id, string hash)
    {
        try
        {
            using var stream = file.OpenReadStream();

            var blobName = $"{id}.{hash}{Path.GetExtension(file.FileName)}";
            var blobClient = containerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync())
                throw new ExceptionDto() { Message = EException.FileExist.DisplayName() };

            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
        catch (Exception ex)
        {
            throw new ExceptionDto() { Message = ex.Message };
        }
    }

    public static async Task<string> UpateBlobFileAsync(BlobContainerClient containerClient, IFormFile file, Guid id, string hash)
    {
        try
        {
            if (file.Length == 0)
                throw new ExceptionDto() { Message = EException.FileNotSelected.DisplayName() };

            using var stream = file.OpenReadStream();

            var blobName = $"{id}.{hash}{Path.GetExtension(file.FileName)}";
            var blobClient = containerClient.GetBlobClient(blobName);

            if(!await blobClient.ExistsAsync())
                throw new ExceptionDto() { Message = EException.FileNotFound.DisplayName() };

            await blobClient.DeleteAsync();
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
        catch (Exception ex)
        {
            throw new ExceptionDto() { Message = ex.Message };
        }
    }

    public static async Task DeleteBlobFileAsync(BlobContainerClient containerClient, IEnumerable<string> blobNamesToDelete)
    {
        try
        {
            foreach (var item in blobNamesToDelete)
            {
                await containerClient.DeleteBlobAsync(item);
            }
        }
        catch (Exception ex)
        {
            throw new ExceptionDto() { Message = ex.Message };
        }
    }
}
