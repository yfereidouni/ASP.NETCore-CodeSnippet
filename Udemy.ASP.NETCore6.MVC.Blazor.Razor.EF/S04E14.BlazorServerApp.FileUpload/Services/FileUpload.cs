using Microsoft.AspNetCore.Components.Forms;

namespace S04E14.BlazorServerApp.FileUpload.Services;

public interface IFileUpload
{
    Task UploadFile(IBrowserFile file);
}

public class FileUpload : IFileUpload
{
    private IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<FileUpload> _logger;

    public FileUpload(IWebHostEnvironment webHostEnvironment, ILogger<FileUpload> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    public async Task UploadFile(IBrowserFile file)
    {
        if (file is not null)
        {
            try
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", file.Name);
                using (var stream = file.OpenReadStream())
                {
                    var fileStream = File.Create(uploadPath);
                    await stream.CopyToAsync(fileStream);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
