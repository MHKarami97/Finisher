using Finisher.Application.Interfaces;
using Finisher.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Finisher.Infrastructure.File;

public class FileStorageService(IWebHostEnvironment env, TimeProvider dateTime, IConfiguration configuration) : IFileStorageService
{
    private readonly int _thumbnailSize = configuration.GetValue<int>("ImageSetting:ThumbSize");
    private readonly int _quantity = configuration.GetValue<int>("ImageSetting:Quantity");

    private static readonly HashSet<string> PermittedImageTypes =
    [
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/bmp",
        "image/webp",
        "image/tiff"
    ];

    private static readonly HashSet<string> PermittedVideoTypes =
    [
        "video/mp4",
        "video/avi",
        "video/mpeg",
        "video/quicktime",
        "video/x-ms-wmv",
        "video/x-msvideo",
        "video/webm",
        "video/3gpp",
        "video/ogg"
    ];

    private static readonly HashSet<string> PermittedDocumentTypes =
        ["application/pdf"];

    public async Task<(string FileUrl, string? ThumbnailUrl)> SaveImageOrVideoAsync(IFormFile file, FileType fileType)
    {
        string result;
        string? thumb = null;
        if (PermittedImageTypes.Contains(file.ContentType))
        {
            (result, thumb) = await SaveImageWithThumbAsync(file, fileType);
        }
        else if (PermittedVideoTypes.Contains(file.ContentType))
        {
            result = await SaveVideoAsync(file, fileType);
        }
        else
        {
            throw new ArgumentException(Messages.InvalidFileType);
        }

        return (result, thumb);
    }

    public async Task<string> SaveImageAsync(IFormFile file, FileType fileType)
    {
        if (!PermittedImageTypes.Contains(file.ContentType))
        {
            throw new ArgumentException(Messages.InvalidFileType);
        }

        var fileTypeFolder = fileType.ToString().ToLowerInvariant();
        var dateFolder = dateTime.GetUtcNow().ToFormattedString();
        var relativePath = Path.Combine(FileConsts.MainUploads, fileTypeFolder, dateFolder);
        var fullPath = Path.Combine(env.WebRootPath, relativePath);

        // Define the folder structure: /uploads/{filetype}/{yyyy-MM-dd}/
        Directory.CreateDirectory(fullPath);

        var guid = GuidGenerator.New();
        var fileName = $"{guid}{FileConsts.WebpFormat}";
        var filePath = Path.Combine(fullPath, fileName);

        using var image = await Image.LoadAsync(file.OpenReadStream());
        image.Mutate(x => x.AutoOrient());

        var webpEncoder = new WebpEncoder { Quality = _quantity, FileFormat = WebpFileFormatType.Lossy };
        await image.SaveAsWebpAsync(filePath, webpEncoder);

        return fileName;
    }

    public async Task<string> SaveFileAsync(IFormFile file, FileType fileType)
    {
        if (!PermittedDocumentTypes.Contains(file.ContentType))
        {
            throw new ArgumentException(Messages.InvalidFileType);
        }

        var fileTypeFolder = fileType.ToString().ToLowerInvariant();
        var dateFolder = dateTime.GetUtcNow().ToFormattedString();
        var relativePath = Path.Combine(FileConsts.MainUploads, fileTypeFolder, dateFolder);
        var fullPath = Path.Combine(env.WebRootPath, relativePath);

        Directory.CreateDirectory(fullPath);

        var guid = GuidGenerator.New();
        var fileName = $"{guid}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
        var filePath = Path.Combine(fullPath, fileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return fileName;
    }

    public async Task<(string FileUrl, string ThumbnailUrl)> SaveImageWithThumbAsync(IFormFile file, FileType fileType)
    {
        if (!PermittedImageTypes.Contains(file.ContentType))
        {
            throw new ArgumentException(Messages.InvalidFileType);
        }

        var fileTypeFolder = fileType.ToString().ToLowerInvariant();
        var dateFolder = dateTime.GetUtcNow().ToFormattedString();
        var relativePath = Path.Combine(FileConsts.MainUploads, fileTypeFolder, dateFolder);
        var fullPath = Path.Combine(env.WebRootPath, relativePath);

        // Define the folder structure: /uploads/{filetype}/{yyyy-MM-dd}/
        Directory.CreateDirectory(fullPath);

        var guid = GuidGenerator.New();
        var fileName = $"{guid}{FileConsts.WebpFormat}";
        var thumbnailFileName = $"{guid}{FileConsts.ThumbPostfix}{FileConsts.WebpFormat}";
        var filePath = Path.Combine(fullPath, fileName);

        using var image = await Image.LoadAsync(file.OpenReadStream());
        image.Mutate(x => x.AutoOrient());

        var webpEncoder = new WebpEncoder { Quality = _quantity, FileFormat = WebpFileFormatType.Lossy };
        await image.SaveAsWebpAsync(filePath, webpEncoder);

        var thumbnailPath = Path.Combine(fullPath, thumbnailFileName);

        var thumbImage = image.Clone(ctx => ctx.Resize(new ResizeOptions { Size = new Size(_thumbnailSize, _thumbnailSize), Mode = ResizeMode.Max }));
        await thumbImage.SaveAsWebpAsync(thumbnailPath, webpEncoder);

        return (fileName, thumbnailFileName);
    }

    public async Task<string> SaveVideoAsync(IFormFile file, FileType fileType)
    {
        if (!PermittedVideoTypes.Contains(file.ContentType))
        {
            throw new ArgumentException(Messages.InvalidFileType);
        }

        var fileTypeFolder = fileType.ToString().ToLowerInvariant();
        var dateFolder = dateTime.GetUtcNow().ToFormattedString();
        var relativePath = Path.Combine(FileConsts.MainUploads, fileTypeFolder, dateFolder);
        var fullPath = Path.Combine(env.WebRootPath, relativePath);

        Directory.CreateDirectory(fullPath);

        var guid = GuidGenerator.New();
        var outputFileName = $"{guid}{FileConsts.Mp4Format}";
        var outputFilePath = Path.Combine(fullPath, outputFileName);

        var tempFilePath = Path.Combine(Path.GetTempPath(), $"{guid}{FileConsts.TempPostfix}{Path.GetExtension(file.FileName)}");
        await using (var stream = new FileStream(tempFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var conversion = await Xabe.FFmpeg.FFmpeg.Conversions.FromSnippet.Convert(tempFilePath, outputFilePath);
        await conversion.Start();

        System.IO.File.Delete(tempFilePath);

        return outputFileName;
    }
}
