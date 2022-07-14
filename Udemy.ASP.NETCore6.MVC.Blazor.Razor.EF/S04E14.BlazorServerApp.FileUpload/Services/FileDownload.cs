using Microsoft.AspNetCore.StaticFiles;
using Microsoft.JSInterop;

namespace S04E14.BlazorServerApp.FileUpload.Services;

public interface IFileDownload
{
    Task<List<string>> GetUploadedFiles();
    Task DownloadFile(string url);
}

public class FileDownload : IFileDownload
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IJSRuntime _js;

    public FileDownload(IWebHostEnvironment webHostEnvironment, IJSRuntime js)
    {
        _webHostEnvironment = webHostEnvironment;
        _js = js;
    }

    public async Task DownloadFile(string url)
    {
        await _js.InvokeVoidAsync("downloadFile", url);
    }

    public async Task<List<string>> GetUploadedFiles()
    {
        var bas64Urls = new List<string>();
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
        var files = Directory.GetFiles(uploadPath);

        if (files is not null && files.Length > 0)
        {
            foreach (var file in files)
            {
                using (var fileInput = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    var memoryStream = new MemoryStream();
                    await fileInput.CopyToAsync(memoryStream);

                    memoryStream.Position = 0;
                    var buffer = memoryStream.ToArray();
                    var fileType = GetMimeTypeForFileExtension(file);

                    bas64Urls.Add($"data:{fileType}; base64, {Convert.ToBase64String(buffer)}");
                }
            }
        }
        return bas64Urls;
    }

    private string GetMimeTypeForFileExtension(string filePath)
    {
        const string DefaultContentType = "application/octed-stream";

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(filePath, out string contentType))
        {
            contentType = DefaultContentType;
        }

        return contentType;
    }
}
