using System.Text;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        // private field
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        // constructor
        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        // check if personAddRequest is null 
        // Validate all properties of PersonAddRequest
        // Convert PersonAddRequest from PersonAddRequest type to Person 
        // Generate new PersonID 
        // Then add it to List<Person> 
        // Return PersonResponse Object with generated PersonID 
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
                throw new ArgumentNullException(nameof(personAddRequest));

            // if (String.IsNullOrEmpty(personAddRequest.PersonName))
            //     throw new ArgumentException("PersonName Can't be blank");

            // if (String.IsNullOrWhiteSpace(personAddRequest.Email))
            //     throw new ArgumentException("Email Can't be blank");

            // string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            // if(!Regex.IsMatch(personAddRequest.Email, pattern))
            //     throw new ArgumentException("Email is not proper");
            
            // Model validation
            ValidationHelper.ModelValidation(personAddRequest);
            if (personAddRequest.DateOfBirth > DateTime.Now)
                throw new ArgumentException("Date Of Birth cannot be greater than this day! .");

            // convert person add request into person type
            Person person = personAddRequest.ToPerson();

            // Generate personID
            person.PersonID = Guid.NewGuid();

            // Add person object to the list<person>
            _persons.Add(person);

            // convert the person object to PersonResponse type
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            List<Person> persons = _persons;

            List<PersonResponse> final_response = new List<PersonResponse>();
            foreach (Person p in persons)
            {
                final_response.Add(PersonExtensions.ToPersonResponse(p));
            }
            return final_response;
        }
    }
}
