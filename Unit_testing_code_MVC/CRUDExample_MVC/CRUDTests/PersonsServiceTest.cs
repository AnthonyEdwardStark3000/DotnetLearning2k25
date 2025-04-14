using System.Text;
using System.Collections.Generic;
using Xunit;
using ServiceContracts;
using Services;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Reflection;
using Entities;
using System.Net;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        // Private fields
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        //Constructor
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonsService();
            _countriesService = new CountriesService(false);
            _testOutputHelper = testOutputHelper;
        }

        #region Add Person

        // When we supply null value as PersonAddRequest, it should throw ArgumentNullException.
        [Fact]
        public void AddPerson_NullPerson()
        {
            // 1. Arrange
            PersonAddRequest? personAddRequest = null;
            // 2. Act
            // 3. Assert
            Assert.Throws<ArgumentNullException>(() => { _personService.AddPerson(personAddRequest); });
        }

        // when we supply personName as null it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            // 1.Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null };
            // 2.Act
            // 3.Assert
            Assert.Throws<ArgumentException>(() => { _personService.AddPerson(personAddRequest); });
        }

        // when we supply proper Person details it should insert the person into the persons list and should return the
        // object of PersonResponse, which includes with the newly generated PersonID

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            // 1. Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Person1",
                Email = "person1@gmail.com",
                DateOfBirth = DateTime.UtcNow,
                Gender = GenderOptions.Male,
                CountryID = Guid.NewGuid(),
                Address = "XYZ street",
                ReceiveNewsLetters = false
            };
            // 2. Act
            PersonResponse actual_person_response_from_add = _personService.AddPerson(personAddRequest);
            List<PersonResponse> persons_list = _personService.GetAllPersons();
            // 3. Assert
            Assert.True(actual_person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(actual_person_response_from_add, persons_list);
        }

        [Fact]
        public void AddPerson_ImproperEmailID()
        {
            // 1. Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest() { Email = "simpleEmail" };
            // 2. Act
            // 3. Assert
            Assert.Throws<ArgumentException>(() => { _personService.AddPerson(personAddRequest); });
        }

        [Fact]
        public void AddPerson_ImproperDOB()
        {
            // 1. Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Suresh",
                Email = "simpleEmail@gmail.com",
                DateOfBirth = DateTime.UtcNow.Date.AddDays(1)
            };
            // 2. Act
            // 3. Assert
            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddRequest));
        }
        #endregion

        #region Get Person By Person ID
        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            // If we supply personID null it should return null as PersonResponse
            // 1.Arrange
            Guid? personID = null;
            // 2.Act
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(personID);
            // 3.Assert
            Assert.Null(person_response_from_get);
        }

        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            // If we supply personID it should return the person detail as PersonResponse
            // 1.Arrange
            CountryAddRequest country_request = new CountryAddRequest() { CountryName = "J_PAN" };
            // 2.Act
            CountryResponse country_response = _countriesService.AddCountry(country_request);
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Person1",
                Email = "person1@gmail.com",
                DateOfBirth = DateTime.UtcNow,
                Gender = GenderOptions.Male,
                CountryID = Guid.NewGuid(),
                Address = "XYZ street",
                ReceiveNewsLetters = false
            };
            personAddRequest.CountryID = country_response.CountryID;
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_add.PersonID);
            // 3.Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }
        #endregion 

        #region Get All Persons
        // The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // 1. Arrange
            // 2. Act
            List<PersonResponse> persons_from_get = _personService.GetAllPersons();
            // 3. Assert
            Assert.Empty(persons_from_get);
        }

        // GetAllPersons() should return all the Persons we add before calling the GetAllPersons method
        [Fact]
        public void GetAddPersons_AddFewPersons()
        {
            // 1. Arrange    
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Person1", Email = "Person1@gmail.com", Gender = GenderOptions.Male, Address = "person1Address", CountryID = country_response_1.CountryID, ReceiveNewsLetters = false, DateOfBirth = DateTime.Parse("2025-03-30") };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Person2", Email = "Person2@gmail.com", Gender = GenderOptions.Female, Address = "person2Address", CountryID = country_response_2.CountryID, ReceiveNewsLetters = true, DateOfBirth = DateTime.Parse("2025-01-12") };
            // 2. Act
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2 };
            List<PersonResponse> person_responses_from_add = new List<PersonResponse>() { };
            foreach (PersonAddRequest person in person_requests)
            {
                person_responses_from_add.Add(_personService.AddPerson(person));
            }

            // Print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_responses_from_add)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> person_response_from_get = _personService.GetAllPersons();
            _testOutputHelper.WriteLine("Actual : ");

            // Print person_response_from_get_all
            foreach (PersonResponse person_response in person_response_from_get)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response.ToString());
            }
            // 3. Assert  
            foreach (PersonResponse person_response in person_responses_from_add)
            {
                Assert.Contains(person_response, person_response_from_get);
            }
        }
        #endregion

        #region Get Filtered Persons
        // If the search text is empty and search by is "PersonName" , it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // 1. Arrange    
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Person1", Email = "Person1@gmail.com", Gender = GenderOptions.Male, Address = "person1Address", CountryID = country_response_1.CountryID, ReceiveNewsLetters = false, DateOfBirth = DateTime.Parse("2025-03-30") };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Person2", Email = "Person2@gmail.com", Gender = GenderOptions.Female, Address = "person2Address", CountryID = country_response_2.CountryID, ReceiveNewsLetters = true, DateOfBirth = DateTime.Parse("2025-01-12") };
            // 2. Act
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2 };
            List<PersonResponse> person_responses_from_add = new List<PersonResponse>() { };
            foreach (PersonAddRequest person in person_requests)
            {
                person_responses_from_add.Add(_personService.AddPerson(person));
            }

            // Print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_responses_from_add)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> person_response_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName), "");
            _testOutputHelper.WriteLine("Actual : ");

            // Print person_response_from_get_all
            foreach (PersonResponse person_response in person_response_from_search)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response.ToString());
            }
            // 3. Assert  
            foreach (PersonResponse person_response in person_responses_from_add)
            {
                Assert.Contains(person_response, person_response_from_search);
            }
        }

        // If we add some persons and then search based on personName with some search string It should return the 
        // matching Persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            // 1. Arrange    
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Person1", Email = "Person1@gmail.com", Gender = GenderOptions.Male, Address = "person1Address", CountryID = country_response_1.CountryID, ReceiveNewsLetters = false, DateOfBirth = DateTime.Parse("2025-03-30") };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Person2", Email = "Person2@gmail.com", Gender = GenderOptions.Female, Address = "person2Address", CountryID = country_response_2.CountryID, ReceiveNewsLetters = true, DateOfBirth = DateTime.Parse("2025-01-12") };
            // 2. Act
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2 };
            List<PersonResponse> person_responses_from_add = new List<PersonResponse>() { };
            foreach (PersonAddRequest person in person_requests)
            {
                person_responses_from_add.Add(_personService.AddPerson(person));
            }

            // Print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_responses_from_add)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> person_response_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName), "ma");
            _testOutputHelper.WriteLine("Actual : ");

            // Print person_response_from_get_all
            foreach (PersonResponse person_response in person_response_from_search)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response.ToString());
            }
            // 3. Assert  
            foreach (PersonResponse person_response in person_responses_from_add)
            {
                if (!String.IsNullOrEmpty(person_response.PersonName) && person_response.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(person_response, person_response_from_search);
                }
            }
        }
        #endregion

        #region Get sorted persons
        // When we sort based on personsName in DESC , it should return persons list in descending order on peron Name
        public void GetSortedPersons()
        {
            // 1. Arrange    
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Person1", Email = "Person1@gmail.com", Gender = GenderOptions.Male, Address = "person1Address", CountryID = country_response_1.CountryID, ReceiveNewsLetters = false, DateOfBirth = DateTime.Parse("2025-03-30") };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Person2", Email = "Person2@gmail.com", Gender = GenderOptions.Female, Address = "person2Address", CountryID = country_response_2.CountryID, ReceiveNewsLetters = true, DateOfBirth = DateTime.Parse("2025-01-12") };
            // 2. Act
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2 };
            List<PersonResponse> person_responses_from_add = new List<PersonResponse>() { };
            foreach (PersonAddRequest person in person_requests)
            {
                person_responses_from_add.Add(_personService.AddPerson(person));
            }

            // Print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_responses_from_add)
            {
                // Override the ToString() method in PersonResponse
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> allPersons = _personService.GetAllPersons();
            List<PersonResponse> person_response_from_sort = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.ASC);
            _testOutputHelper.WriteLine("Actual : ");
            person_responses_from_add = person_responses_from_add.OrderByDescending(temp => temp.PersonName).ToList();

            // 3. Assert  
            for (int i = 0; i < person_responses_from_add.Count; i++)
            {
                Assert.Equal(person_responses_from_add[i], person_response_from_sort[i]);
            }

        }
        #endregion

        #region UpdatePerson
        // when we supply null as PersonUpdateRequest, it should throw Argument null exception
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            // 1. Arrange
            personUpdateRequest? personUpdateRequest = null;
            // 2. Act
            Assert.Throws<ArgumentNullException>(
            // 3. Assert
                () => _personService.UpdatePerson(personUpdateRequest));
        }

        // When the person ID is invalid it should throw argument exception
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            // 1. Arrange
            personUpdateRequest? personUpdateRequest = new personUpdateRequest()
            {
                PersonID = new Guid()
            };
            // 2. Act
            Assert.Throws<ArgumentException>(
            // 3. Assert
                () => _personService.UpdatePerson(personUpdateRequest));
        }

        // When the person Name is null it should throw argument exception
        [Fact]
        public void UpdatePerson_NullPersonName()
        {
            // 1. Arrange
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "Sample"
            };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = country_response_from_add.CountryID,
                Email = "check@gmail.com"
            };
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);
            personUpdateRequest? person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;
            // 2. Act
            Assert.Throws<ArgumentException>(
                // 3. Assert
                () => _personService.UpdatePerson(person_update_request));
        }

        // Add the new person and try to update the personName and Email
        [Fact]
        public void UpdatePerson_PersonFullDetails()
        {
            // 1. Arrange
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "Sample"
            };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = country_response_from_add.CountryID,
                Address = "Address",
                Email = "Email@gmail.com",
                DateOfBirth = DateTime.UtcNow,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);
            personUpdateRequest? person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";
            person_update_request.Email = "William@gmail.com";
            // 2. Act
            PersonResponse person_response_from_update = _personService.UpdatePerson(person_update_request);
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_update.PersonID);
            // 3. Assert
            Assert.Equal(person_response_from_get, person_response_from_update);
        }
        #endregion

        #region DeletePerson
        // If you supply an Valid PersonID it should return false
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            // 1. Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "USA" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "Name", Address = "XYZ street", CountryID = country_response_from_add.CountryID, DateOfBirth = Convert.ToDateTime("2000-01-12"), Email = "check@gmail.com", Gender = GenderOptions.Male, ReceiveNewsLetters = false };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);
            // 2. Act
            bool is_deleted = _personService.DeletePerson(person_response_from_add.PersonID);
            // 3. Assert
            Assert.True(is_deleted);
        }

        // If you supply an Invalid PersonID it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            // 1. Arrange
            // 2. Act
            bool is_deleted = _personService.DeletePerson(Guid.NewGuid());
            // 3. Assert
            Assert.False(is_deleted);
        }
        #endregion
    }
}
