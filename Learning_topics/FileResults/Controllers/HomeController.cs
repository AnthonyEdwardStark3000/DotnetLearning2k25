using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    [HttpGet("file-download")]
    public VirtualFileResult Index()
    {
        // return new VirtualFileResult("/zoho.png", "image/png");
        return File("U:/Dotnet_2k25/Learning_topics/FileResults/External_file/RE_GT650.jpg", "image/png");
    }


    [HttpGet("file-outside-download")]
    public PhysicalFileResult outside()
    {
        // return new PhysicalFileResult("U:/Dotnet_2k25/Learning_topics/FileResults/External_file/RE_GT650.jpg", "image/png");

        return PhysicalFile("U:/Dotnet_2k25/Learning_topics/FileResults/External_file/RE_GT650.jpg", "image/png");
    }

    [HttpGet("file-byte-download")]
    public FileContentResult returnByteContent()
    {
        byte[] content = System.IO.File.ReadAllBytes("U:/Dotnet_2k25/Learning_topics/FileResults/External_file/ChatGPT Image Apr 20, 2025, 03_20_28 PM.png");
        // return new FileContentResult(content, "image/png");
        return File(content,"image/png");
    }

    
}