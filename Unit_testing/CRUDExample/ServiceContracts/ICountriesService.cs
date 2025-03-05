using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity 
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country Object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country Object to be added</param>
        /// <returns>Returns the country Object as response</returns>
       public CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
        /// <summary>
        /// Returns all countries from the list.
        /// </summary>
        /// <returns>All countries as List<CountryResponse></returns>
       public List<CountryResponse> GetAllCountries();
        /// <summary>
        /// Returns CountryResponse object based on the given countryID and null if no result is found.
        /// It does not throw an exception if the country is not found.
        /// </summary>
        /// <returns>Matching Country as countryResponse Object<CountryResponse></returns>
        CountryResponse? GetCountryByCountryID(Guid? CountryID);

    }
}