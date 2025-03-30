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

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        // Private fields
        private readonly IPersonsService _personService;

        //Constructor
        public PersonsServiceTest()
        {
            _personService = new PersonsService();
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


    }
}
