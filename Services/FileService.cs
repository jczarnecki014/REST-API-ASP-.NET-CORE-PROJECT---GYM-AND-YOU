using GymAndYou.Exceptions;
using GymAndYou.Models.Query_Models;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection.Metadata.Ecma335;

namespace GymAndYou.Services
{
    public interface IFileService
    {
        FileResoult GetFile(string fileName);
        string UploadFile(IFormFile file);
    }

    public class FileService : IFileService
    {
        public FileResoult GetFile(string fileName)
        {
        var filePath = this.GetFilePath(fileName);

        if (!File.Exists(filePath))
        {
        throw new FileNotFound("File witch this name doesn't exist in the server resources");
        }

        var file = File.ReadAllBytes(filePath);

        var ContentTypeProvider = new FileExtensionContentTypeProvider();
        ContentTypeProvider.TryGetContentType(filePath, out var contentType);

        return new FileResoult()
        {
            fileContents = file,
            contentType = contentType,
            fileName = fileName,
        };
        }

        public string UploadFile(IFormFile file)
        {
            if (file is null || file.Length < 0)
            {
            throw new FileNotFound("File is null or empty");
            }

            var filePath = this.GetFilePath(file.FileName);

            using (var newStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(newStream);
            }

            return file.FileName;
        }


        private string GetFilePath(string FileName)
        {
        var solutionDirectory = Directory.GetCurrentDirectory();
        return $"""{solutionDirectory}\PrivateFiles\{FileName}""";
        }
    }
}
