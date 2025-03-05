using System.Data;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        public CountriesServiceTest() {
              _countriesService = new CountriesService();
        }

        #region AddCountry_Test
        // 1.When CountryAddRequest is null it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry() {
            // Arrange
            CountryAddRequest? request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() => { 
            //lambda expressing to check the Assert
            //Act
            _countriesService.AddCountry(request);
            });
            }
        // 2.When CountryName is null it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName = null};
            //Assert
            Assert.Throws<ArgumentException>(() => {
                //lambda expressing to check the Assert
                //Act
                _countriesService.AddCountry(request);
            });
        }
        // 3.When CountryName is duplicate it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsDuplicate()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName = "USA"};
            CountryAddRequest request1 = new CountryAddRequest() { CountryName = "USA"};
            //Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _countriesService.AddCountry(request);
                _countriesService.AddCountry(request1);
            });
        }
        // 4.When CountryName is proper it should insert the same into the list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName="Japan"};
            // Act
            CountryResponse response = _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();
            // Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }
        #endregion

        #region GetAllCountries Response
        //The list of countries should be empty by default
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Acts
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_country_response_list);
        }

        //The countries added in the List should be displayed
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>() {
                new CountryAddRequest(){
                    CountryName="USA",
                },
                new CountryAddRequest(){
                    CountryName="china",
                },
            };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list) {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }
            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

            //read each element from countries_list_from_add_country
            foreach (CountryResponse expected_country in countries_list_from_add_country) {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }
        #endregion

        // If we supply the null countryID then it should return null
        #region GetCountryByCountryID
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? CountryID = null;
            //Act
            CountryResponse? country_response_from_get_method = _countriesService.GetCountryByCountryID(CountryID);
            //Assert
            Assert.Null(country_response_from_get_method);
        }
        #endregion

        // If we supply the proper countryID then it should return the country details
        #region GetCountryByCountryID
        [Fact]
        public void GetCountryByCountryID_ProperCountryID() { 
            //Arrange
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "USA" };
            CountryResponse country_response_from_add_request = _countriesService.AddCountry(country_add_request);
            //Act
            CountryResponse country_response_from_get_request = _countriesService.GetCountryByCountryID(country_response_from_add_request.CountryID);
            //Assert
            Assert.Equal(country_response_from_add_request,country_response_from_get_request);
        }
        #endregion
    }
}
