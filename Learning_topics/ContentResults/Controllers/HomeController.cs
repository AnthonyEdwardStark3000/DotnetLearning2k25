using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    [Route("/")]
    public ContentResult Index()
    {
        return new ContentResult() { Content = "Hope this makes you understand , you can do a lot more than what you think you can.", ContentType = "text/plain" };
    }
}