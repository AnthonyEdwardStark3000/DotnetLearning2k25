using System.Text;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;

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
            if (personAddRequest.DateOfBirth > DateTime.UtcNow)
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
            // List<Person> persons = _persons;

            // foreach (Person p in persons)
            // {
            //     final_response.Add(PersonExtensions.ToPersonResponse(p));
            // }
            List<PersonResponse> final_response = _persons.Select(person => person.ToPersonResponse()).ToList();
            return final_response;
        }

        public PersonResponse GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;
            Person person = _persons.FirstOrDefault((person) => person.PersonID == personID);
            if (person == null)
                return null;
            return person.ToPersonResponse();
        }

        // check if searchby is not null, Get matching persons from List<Person> based on given searchby and searchstring
        // Convert the matching persons to PersonResponse type and return the list of PersonResponse
        // if no matching persons found, return empty list 
        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;
            if (String.IsNullOrEmpty(searchBy) || String.IsNullOrEmpty(searchString))
                return matchingPersons;
            switch (searchBy)
            {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(person => String.IsNullOrEmpty(person.PersonName) ? true : person.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(person => String.IsNullOrEmpty(person.Email) ? true : person.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(person => !person.DateOfBirth.HasValue ? true : person.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(person => String.IsNullOrEmpty(person.Gender) ? true : person.Gender.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(person => !person.CountryID.HasValue ? true : person.CountryID.Value.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                default:
                    matchingPersons = allPersons;
                    break;
            }
            return matchingPersons;
        }

        //
        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (String.IsNullOrEmpty(sortBy))
                return allPersons;

            // switch expression instead of using switch case   
            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                // Default case

                _ => allPersons
            };
            return sortedPersons;
        }

        public PersonResponse UpdatePerson(personUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));
            // validation 
            ValidationHelper.ModelValidation(personUpdateRequest);
            // Get matching person Object to update
            Person? matchingPerson = _persons.FirstOrDefault(temp=>temp.PersonID == personUpdateRequest.PersonID);
            if(matchingPerson==null)
                throw new ArgumentException("Given person ID doesn't exist");
            // Update details 
            matchingPerson.PersonName = personUpdateRequest.PersonName;   
            matchingPerson.CountryID = personUpdateRequest.CountryID;   
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;   
            matchingPerson.Email = personUpdateRequest.Email;   
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();   
            matchingPerson.Address = personUpdateRequest.Address;   
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;   

            return matchingPerson.ToPersonResponse();
        }
    }
}
