using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
    public class PersonsController : Controller
    {
        //  private fields
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        // constructor
        public PersonsController(IPersonsService personsService, ICountriesService countriesService)
        {
            _personsService = personsService;
            _countriesService = countriesService;
        }
        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            // Searching
            ViewBag.SearchFields = new Dictionary<String, String>(){
                {nameof(PersonResponse.PersonName),"Person Name"},
                {nameof(PersonResponse.Email),"Email"},
                {nameof(PersonResponse.DateOfBirth),"Date Of Birth"},
                {nameof(PersonResponse.Age),"Age"},
                {nameof(PersonResponse.Gender),"Gender"},
                {nameof(PersonResponse.CountryID),"Country ID"},
                {nameof(PersonResponse.Address),"Address"},
            };
            List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            // Sorting
            List<PersonResponse> sorted_persons = _personsService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder;
            return View(sorted_persons); // calling Views/ Persons/ Index.cshtml
        }

        // Executes when the user clicks on "Create Person" hyperlink (while opening the create view)
        [Route("persons/create")]
        [HttpGet]
        public IActionResult Create()
        {
            List<CountryResponse> countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries;
            return View();
        }
    }
}