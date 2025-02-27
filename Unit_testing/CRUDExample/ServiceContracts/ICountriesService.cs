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
       public CountryResponse AddCountry(CountryAddRequest?countryAddRequest);
    }
}
