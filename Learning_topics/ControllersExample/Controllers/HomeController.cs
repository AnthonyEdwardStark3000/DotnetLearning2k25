using Microsoft.AspNetCore.Mvc;

namespace ControllersExample
{
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public string method1()
        {
            return "Done...";
        }
    }
}
