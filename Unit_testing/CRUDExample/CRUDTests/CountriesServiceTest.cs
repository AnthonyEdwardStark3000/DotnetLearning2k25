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
            // Assert
            Assert.True(response.CountryID != Guid.Empty);
        }

    }
}
