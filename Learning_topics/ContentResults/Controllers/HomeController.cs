using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("/")]
    public ContentResult Index()
    {
        // return new ContentResult() { Content = "Hope this makes you understand , you can do a lot more than what you think you can.", ContentType = "text/plain" };
        return Content("<h1>Just do it....</h1>","text/html");
    }
}