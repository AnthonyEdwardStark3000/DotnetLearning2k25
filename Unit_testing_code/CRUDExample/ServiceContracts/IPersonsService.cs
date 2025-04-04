﻿using System.Text;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person Entity
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new person into the List of persons available
        /// </summary>
        /// <param name="personAddRequest">Person to add</param>
        /// <returns>Returns the same person Details along with the newly generated PersonID</returns>
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest);


        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of Person Response type</returns>
        public List<PersonResponse> GetAllPersons();


        /// <summary>
        /// Returns the person based on the given person ID
        /// </summary>
        /// <returns>Returns an object of the matching person in Person Response type</returns>
        public PersonResponse? GetPersonByPersonID(Guid? personID);
    }
}
