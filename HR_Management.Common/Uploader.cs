using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HR_Management.Common;

public class Uploader
{
    private static readonly IWebHostEnvironment _webHostEnvironment;
    private static string _webRoot;

    public Uploader(string webRoot)
    {
        _webRoot = webRoot;
    }

    /// <summary>
    ///     Uploads each file you want to add in every folder or route
    /// </summary>
    /// <param name="file"></param>
    /// <param name="baseFolder"></param>
    /// <returns></returns>
    public async Task<UploadDto> UploadFile(IFormFile file, string baseFolder)
    {
        if (file == null)
            return new UploadDto
            {
                Status = false,
                FileNameAddress = ""
            };

        var now = DateTime.UtcNow;

        //making folder based on current year & month
        var dateFolder = Path.Combine(now.Year.ToString(), now.Month.ToString("00"));

        //removing spare / from baseFolder
        baseFolder = baseFolder.TrimStart('/').Replace("//", "/");

        var finalFolderPath = Path.Combine(_webRoot, baseFolder, dateFolder);
        if (!Directory.Exists(finalFolderPath))
            Directory.CreateDirectory(finalFolderPath);

        var uniqueFileName = $"{now.Ticks}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(finalFolderPath, uniqueFileName);

        //making unique file name
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return new UploadDto
        {
            // relative address to add in database
            FileNameAddress = Path.Combine("/", baseFolder, dateFolder, uniqueFileName).Replace("\\", "/"),
            Status = true
        };
    }

    public class UploadDto
    {
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}