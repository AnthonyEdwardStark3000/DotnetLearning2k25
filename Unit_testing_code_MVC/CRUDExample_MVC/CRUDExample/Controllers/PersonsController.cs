using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUDExample.Controllers
{
    public class PersonsController : Controller
    {
        //  private fields
        private readonly IPersonsService _personsService;

        // constructor
        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }
        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy,string? searchString)
        {
            ViewBag.SearchFields = new Dictionary<String,String>(){
                {nameof(PersonResponse.PersonName),"Person Name"},
                {nameof(PersonResponse.Email),"Email"},
                {nameof(PersonResponse.DateOfBirth),"Date Of Birth"},
                {nameof(PersonResponse.Gender),"Gender"},
                {nameof(PersonResponse.CountryID),"Country ID"},
                {nameof(PersonResponse.Address),"Address"},
            };
            List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy,searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            return View(persons); // calling Views/ Persons/ Index.cshtml
        }
    }
}