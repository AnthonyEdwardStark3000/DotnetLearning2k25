using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    [HttpGet("persons")]
    public JsonResult GetPerson()
    {
        // return "{\"Name\":\"Suresh\"}";
        Person person = new Person()
        {
            id = Guid.NewGuid(),
            FirstName = "check",
            age = 12,
            LastName = "user"
        };

        return new JsonResult(person);
    }
}