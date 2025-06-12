using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using FFMpegCore;
using HiFive.Domain.Models.Music;

namespace HiFive.Presentation;

public class BlobService
{
	private readonly BlobContainerClient _containerClient;
	private readonly StorageSharedKeyCredential _storageCredential;

	public BlobService(IConfiguration config)
	{
		var settings = config.GetSection("Azure") ?? throw new InvalidOperationException("Azure not configured");

		string connectionString = settings["ConnectionString"] ?? throw new InvalidOperationException("ConnectionString not configured");
		string containerName = settings["ContainerName"] ?? throw new InvalidOperationException("ContainerName not configured");

		var blobServiceClient = new BlobServiceClient(connectionString);
		_containerClient = blobServiceClient.GetBlobContainerClient(containerName);
		_containerClient.CreateIfNotExists();

		var connStringParts = connectionString.Split(';');
		string accountName = "";
		string accountKey = "";

		foreach (var part in connStringParts)
		{
			if (part.StartsWith("AccountName=", StringComparison.InvariantCultureIgnoreCase))
				accountName = part.Substring("AccountName=".Length);
			else if (part.StartsWith("AccountKey=", StringComparison.InvariantCultureIgnoreCase))
				accountKey = part.Substring("AccountKey=".Length);
		}

		if (accountName == "" || accountKey == "")
			throw new InvalidOperationException("Connection string must contain AccountName and AccountKey.");

		_storageCredential = new StorageSharedKeyCredential(accountName, accountKey);
	}

	public string GetSasUrl(string blobName, TimeSpan expiryDuration)
	{
		var blobClient = _containerClient.GetBlobClient(blobName);

		var sasBuilder = new BlobSasBuilder
		{
			BlobContainerName = _containerClient.Name,
			BlobName = blobName,
			Resource = "b", // b for blob
			ExpiresOn = DateTimeOffset.Now.Add(expiryDuration)
		};

		sasBuilder.SetPermissions(BlobSasPermissions.Read);

		var sasToken = sasBuilder.ToSasQueryParameters(_storageCredential).ToString();
		var uriBuilder = new UriBuilder(blobClient.Uri) { Query = sasToken };

		return uriBuilder.ToString();
	}

	public async Task<(uint Duration, string Uri)> UploadFileAsync(IFormFile file)
	{
		var tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

		await using (var tempFile = new FileStream(tempFilePath, FileMode.Create))
		{
			await file.CopyToAsync(tempFile);
		}

		var mediaInfo = await FFProbe.AnalyseAsync(tempFilePath);
		var duration = (uint)Math.Round(mediaInfo.Duration.TotalSeconds);

		var blobClient = _containerClient.GetBlobClient($"{DateTime.Now:yyyy-MM-dd} - {file.FileName} - {Guid.NewGuid()}");
		var headers = new BlobHttpHeaders { ContentType = file.ContentType };

		await using (var uploadStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			await blobClient.UploadAsync(uploadStream, new BlobUploadOptions
			{
				HttpHeaders = headers
			});
		}

		File.Delete(tempFilePath);

		return (duration, blobClient.Name);
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
