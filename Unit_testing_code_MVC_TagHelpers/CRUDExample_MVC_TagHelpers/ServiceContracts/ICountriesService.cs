using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Interface for Countries Service, business logic for manipulating Country Entity  
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries 
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>returns country object after including the newly generated countryID</returns>
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Retrieves all the available countries and returns the list of CountryResponse
        /// </summary>
        /// <returns>List<CountryResponse></CountryResponse></returns>
        public List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Gets the country ID and returns the corresponding country as CountryResponse object
        /// </summary>
        /// <param name="countryID">CountryID (guid) to search</param>
        /// <returns>Matching country as CountryResponse</returns>
        public CountryResponse GetCountryByCountryID(Guid? countryID);
    }
}
