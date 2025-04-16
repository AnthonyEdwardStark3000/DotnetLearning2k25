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
        public PersonsService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService(false);
            if (initialize)
            {
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("4661837E-80D8-4EEE-B7D9-66726503A89E"),
                        PersonName = "Shawna",
                        Email = "scoleridge0@fema.gov",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("C19C2F83-4827-4C3C-96F0-E43AD3B2C42E"),
                        Address = "Suite 73",
                        ReceiveNewsLetters = false
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("5E4D895B-57E5-4F19-B0E0-E2A670DF86A7"),
                        PersonName = "Ambrosi",
                        Email = "aspilsburie1@1688.com",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("81E2D75D-86B6-4934-A0ED-28ED41B5F3BA"),
                        Address = "Suite 93",
                        ReceiveNewsLetters = true
                    });

                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("910EDCD9-EA62-40F4-9D47-A1EBFD40B899"),
                        PersonName = "Cly",
                        Email = "cickovitz2@go.com",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Female.ToString(),
                        CountryID = Guid.Parse("28724E19-FE2D-462B-BCC0-A24E862FFB98"),
                        Address = "Apt 771",
                        ReceiveNewsLetters = false
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("63C99364-0E84-4043-8416-8EF80191D0CB"),
                        PersonName = "Irwin",
                        Email = "ihaensel3@wunderground.com",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("B016AE7D-06AE-47C9-83FC-26840F3E3630"),
                        Address = "Room 1271",
                        ReceiveNewsLetters = true
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("B816C990-D9B6-40CD-9652-FBA7115608C1"),
                        PersonName = "Alick",
                        Email = "aanthill4@godaddy.com",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Female.ToString(),
                        CountryID = Guid.Parse("FCC55DF1-F715-41E6-BBC5-3E956EC9065C"),
                        Address = "4th Floor",
                        ReceiveNewsLetters = true
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("A3C6F012-EA20-4096-B985-B990AAE29BAB"),
                        PersonName = "Urbain",
                        Email = "usaiger5@phoca.cz",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("5BE3BF31-FC57-43E9-82FA-53A3D80B1F44"),
                        Address = "Suite 70",
                        ReceiveNewsLetters = false
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("9E99568A-B9F2-4E1B-BCFD-4D9E25E3EEA9"),
                        PersonName = "Rennie",
                        Email = "redbrooke6@ow.ly",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Female.ToString(),
                        CountryID = Guid.Parse("E0F77EB7-0487-4A08-96D5-701717E879A4"),
                        Address = "Suite 63",
                        ReceiveNewsLetters = true
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("FCF8BC4D-DDD7-4A81-A3EC-D10B8733841D"),
                        PersonName = "Carlos",
                        Email = "cjarad7@unblog.fr",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("61780F07-A9AD-4610-A622-965D3B2DDEDF"),
                        Address = "Suite 7",
                        ReceiveNewsLetters = true
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("4937EAFE-14B1-4A91-9FCA-0CA1D0F8BF74"),
                        PersonName = "Edgard",
                        Email = "edomleo8@tripadvisor.com",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("8407A45E-A085-4208-B6ED-0E1F9BD1529A"),
                        Address = "Apt 381",
                        ReceiveNewsLetters = true
                    });
                _persons.Add(
                    new Person()
                    {
                        PersonID = Guid.Parse("B3E679CD-15CA-4618-BE5E-0400E89BB10D"),
                        PersonName = "Isidore",
                        Email = "iickovici9@virginia.edu",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = GenderOptions.Male.ToString(),
                        CountryID = Guid.Parse("BFA5A16D-9D4E-48A4-B141-5E83B8DA6D9C"),
                        Address = "Suite 73",
                        ReceiveNewsLetters = false
                    });
            }
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
                case nameof(PersonResponse.PersonName):
                    matchingPersons = allPersons.Where(person => String.IsNullOrEmpty(person.PersonName) ? true : person.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(PersonResponse.Email):
                    matchingPersons = allPersons.Where(person => String.IsNullOrEmpty(person.Email) ? true : person.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(PersonResponse.DateOfBirth):
                    matchingPersons = allPersons.Where(person => !person.DateOfBirth.HasValue ? true : person.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(PersonResponse.Gender):
                    matchingPersons = allPersons.Where(person => String.IsNullOrEmpty(person.Gender) ? true : person.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(PersonResponse.CountryID):
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
            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null)
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

        public bool DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));
            Person? found_person = _persons.FirstOrDefault(person => person.PersonID == personID);
            if (found_person == null)
                return false;
            _persons.RemoveAll(person => person.PersonID == found_person.PersonID);
            return true;
        }
    }
}
