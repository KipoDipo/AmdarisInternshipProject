using Azure.Storage.Blobs;

namespace HiFive.Presentation;

public class BlobService
{
	private readonly BlobContainerClient _containerClient;

	public BlobService(IConfiguration config)
	{
		var settings = config.GetSection("Azure") ?? throw new InvalidOperationException("Azure not configured");

		string connectionString = settings["ConnectionString"] ?? throw new InvalidOperationException("ConnectionString not configured");
		string containerName = settings["ContainerName"] ?? throw new InvalidOperationException("ContainerName not configured");

		var blobServiceClient = new BlobServiceClient(connectionString);
		_containerClient = blobServiceClient.GetBlobContainerClient(containerName);
		_containerClient.CreateIfNotExists();
	}

	public async Task<string> UploadFileAsync(IFormFile file)
	{
		var blobClient = _containerClient.GetBlobClient(file.FileName);
		using var stream = file.OpenReadStream();
		await blobClient.UploadAsync(stream, overwrite: true);
		return blobClient.Uri.ToString();
	}

	public async Task<(Stream stream, string ContentType)> DownloadFileAsync(string fileName)
	{
		var blobClient = _containerClient.GetBlobClient(fileName);

		if (!await blobClient.ExistsAsync())
			return (null, null);

		var properties = await blobClient.GetPropertiesAsync();
		var contentType = properties.Value.ContentType ?? "application/octet-stream";

		var download = await blobClient.DownloadAsync();
		return (download.Value.Content, contentType);
	}
}
