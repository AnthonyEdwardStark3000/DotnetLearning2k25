using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        // readonly field
        private readonly List<Country> _countries;

        // constructor to initialize the list of countries
        public CountriesService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validations countryAddRequest cannot be null
            if (countryAddRequest==null) {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            if (countryAddRequest.CountryName==null) {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            if (_countries.Where(country => country.CountryName == countryAddRequest.CountryName).Count()>0) {
                throw new ArgumentException("Country Name already exists");
            }
            // convert object from countryAddRequest to Country
            Country country = countryAddRequest.ToCountry();
            // Generate new GUID to the country
            country.CountryID = Guid.NewGuid();
            // Add Country to the list of countries
            _countries.Add(country);
            return country.ToCountryResponse();
        }

        /// <summary>
        /// Converts from country Object to CountryResponse object
        /// </summary>
        /// <returns></returns>
        public List<CountryResponse> GetAllCountries() {
           return _countries.Select(country=>country.ToCountryResponse()).ToList();    
        }

        public CountryResponse GetCountryByCountryID(Guid? countryID)
        {
            if (countryID==null) 
                return null;
            
            Country? country_response_from_list = _countries.FirstOrDefault(country=>country.CountryID==countryID);
            if (country_response_from_list == null)
                return null;
            return country_response_from_list.ToCountryResponse();
        }
    }
}

