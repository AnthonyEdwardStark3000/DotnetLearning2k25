using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        // private field
        private readonly List<Country> _countries;

        // Constructor
        public CountriesService()
        {
            _countries = new List<Country>();
        }
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
    }
}
