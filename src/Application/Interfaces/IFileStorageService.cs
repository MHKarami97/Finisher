using Microsoft.AspNetCore.Http;
using Finisher.Domain.Enums;

namespace Finisher.Application.Interfaces;

public interface IFileStorageService
{
    Task<(string FileUrl, string? ThumbnailUrl)> SaveImageOrVideoAsync(IFormFile file, FileType fileType);
    Task<string> SaveImageAsync(IFormFile file, FileType fileType);
    Task<string> SaveFileAsync(IFormFile file, FileType fileType);
    Task<(string FileUrl, string ThumbnailUrl)> SaveImageWithThumbAsync(IFormFile file, FileType fileType);
    Task<string> SaveVideoAsync(IFormFile file, FileType fileType);
}
