using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        /// <summary>
        /// It is known as a Data store
        /// </summary>
        // private field
        private readonly List<Country> _countries;

        // Constructor
        public CountriesService()
        {
            _countries = new List<Country>();
        }
        #region AddCountry
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Check if countryAddRequest is not null
            // validate all properties of countryAddRequest
            // convert countryAddRequest from CountryAddRequest type to Country
            // Generate new country GUID
            // then add it to List<Country>
            // Return CountryResponse object with generated CountryID

            //validation: countryAddRequest cannot be null
            if (countryAddRequest==null) {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //validation: countryName cannot be null
            if (countryAddRequest.CountryName==null) {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //validation: countryName cannot be duplicate
            if (_countries.Where(country =>  country.CountryName == countryAddRequest.CountryName).Count()>0) {
                throw new ArgumentException("Given country name already exists");
            }

            //convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //generate countryID
            country.CountryID = Guid.NewGuid();

            // add country object into countries list
            _countries.Add(country);
            return country.ToCountryResponse();
        }
        #endregion

        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>All countries from the list<CountryResponse></returns>
        /// <exception cref="NotImplementedException"></exception>
        #region Get All Countries
        public List<CountryResponse> GetAllCountries()
        {
            // LINQ statement with lambda expression.
            return _countries.Select(country=>country.ToCountryResponse()).ToList();
            // .ToList() converts the enumerable object into a list.
        }
        #endregion

        /// <summary>
        /// Get country details based on the entered country GUID
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        #region Get Country by countryID
        public CountryResponse? GetCountryByCountryID(Guid? CountryID)
        {
            // Receives the countryID checks with the corresponding countryID in the list of countries if there is a match
            // returns that country object as a response, by converting the country object from Country type to CountryResponse type.
            CountryResponse? result = null;
            if (!CountryID.Equals(null)) {
               Country? country_response_from_list = _countries.FirstOrDefault(country => country.CountryID == CountryID);
                if (country_response_from_list != null) { 
                    result = country_response_from_list.ToCountryResponse();
                }
            }
            return result;
        }
        #endregion
    }
}       
