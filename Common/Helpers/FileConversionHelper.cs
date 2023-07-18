using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;


namespace Common.Helpers;

public static class FileConversionHelper
{
    public static async Task<string> ConvertToBase64Async(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var base64Content = Convert.ToBase64String(memoryStream.ToArray());

        return base64Content;
    }

    public static async Task<Stream> ConvertToStreamAsync(IFormFile file)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        return stream;
    }

    public static Stream ConvertToStream(string base64Content)
    {
        var content = Convert.FromBase64String(base64Content);
        var stream = new MemoryStream(content);
        return stream;
    }

    //public static async Task<FileContentResult> ConvertToContentAsync(IFormFile file)
    //{
    //    var stream = await ConvertToStreamAsync(file);

    //    var fileContent = new StreamContent(stream);
    //    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
    //    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
    //    {
    //        FileName = file.FileName,
    //    };

    //    return new FileContentResult(stream.ToArray(), file.ContentType)
    //    {
    //        FileDownloadName = file.FileName,
    //    };
    //}

    //public static async Task<FileContentResult> ConvertToContentAsync(string base64Content, string contentType, string fileName)
    //{
    //    var stream = ConvertToStream(base64Content);

    //    var fileContent = new StreamContent(stream);
    //    fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
    //    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
    //    {
    //        FileName = fileName,
    //    };

    //    return new FileContentResult(stream.ToArray(), contentType)
    //    {
    //        FileDownloadName = fileName,
    //    };
    //}
}