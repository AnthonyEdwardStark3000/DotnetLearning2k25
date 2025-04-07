using System.Text;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

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

        /// <summary>
        /// Returns all person objects that matches with the given search field and search field
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all the matching persons based on the given search field and search string</returns> 
        /// <summary>
        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

        
        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents List of persons to sort</param>
        /// <param name="sortBy">name of the property (key) based on which the persons should be sorted</param>
        /// <param name="sortOrder">ASC / DESC</param>
        /// <returns>Returns the List PersonResponse after sorting the persons</returns> 
        /// <summary>
        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
    }
}
