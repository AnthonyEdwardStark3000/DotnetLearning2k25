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
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>{
                new Country()
                {
                    CountryID = Guid.Parse("C19C2F83 - 4827 - 4C3C - 96F0 - E43AD3B2C42E"),
                    CountryName = "India"
                },
                new Country()
                {
                    CountryID = Guid.Parse("81E2D75D - 86B6 - 4934 - A0ED - 28ED41B5F3BA"),
                    CountryName = "China"
                },
                new Country()
                {
                    CountryID = Guid.Parse("28724E19 - FE2D - 462B - BCC0 - A24E862FFB98"),
                    CountryName = "Indonesia"
                },
                new Country()
                {
                    CountryID = Guid.Parse("B016AE7D - 06AE - 47C9 - 83FC - 26840F3E3630"),
                    CountryName = "Brazil"
                },
                new Country()
                {
                    CountryID = Guid.Parse("FCC55DF1 - F715 - 41E6 - BBC5 - 3E956EC9065C"),
                    CountryName = "USA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("5BE3BF31 - FC57 - 43E9 - 82FA - 53A3D80B1F44"),
                    CountryName = "Europe"
                },
                new Country()
                {
                    CountryID = Guid.Parse("E0F77EB7 - 0487 - 4A08 - 96D5 - 701717E879A4"),
                    CountryName = "Chicago"
                },
                new Country()
                {
                    CountryID = Guid.Parse("61780F07 - A9AD - 4610 - A622 - 965D3B2DDEDF"),
                    CountryName = "Africa"
                },
                new Country()
                {
                    CountryID = Guid.Parse("8407A45E - A085 - 4208 - B6ED - 0E1F9BD1529A"),
                    CountryName = "Paris"
                },
                new Country()
                {
                    CountryID = Guid.Parse("BFA5A16D - 9D4E-48A4 - B141 - 5E83B8DA6D9C"),
                    CountryName = "France"
                }
                });
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validations countryAddRequest cannot be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            if (_countries.Where(country => country.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
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
        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country_response_from_list = _countries.FirstOrDefault(country => country.CountryID == countryID);
            if (country_response_from_list == null)
                return null;
            return country_response_from_list.ToCountryResponse();
        }
    }
}

