using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest() { 
            _countriesService = new CountriesService();
        }

        #region Add countries
        //Test cases for AddCountry

        // 1. When CountryAddRequest is Null , it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // 1. Arrange
            CountryAddRequest? addRequest = null;
            // 3. Assert
            Assert.Throws<ArgumentNullException>(() =>
            // 2. Act
            _countriesService.AddCountry(addRequest));
        }

        // 2. When CountryName is Null , it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // 1. Arrange
            CountryAddRequest? addRequest = new CountryAddRequest() { CountryName = null };
            // 3. Assert
            Assert.Throws<ArgumentException>(() =>
            // 2. Act
            _countriesService.AddCountry(addRequest));
        }

        // 3. When CountryName is Duplicate , it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // 1. Arrange
            CountryAddRequest? addRequest = new CountryAddRequest() { CountryName = "USA" };

            // 3. Assert
            Assert.Throws<ArgumentException>(() => {
                // 2. Act
                _countriesService.AddCountry(addRequest);
                _countriesService.AddCountry(addRequest);
            }
            );
        }

        // 4. When you supply proper CountryName it should add the country to the existing list of countries
        [Fact]
        public void AddCountry_Proper_CountryDetails()
        {
            // 1. Arrange
            CountryAddRequest? addRequest1 = new CountryAddRequest() { CountryName = "Japan" };
            CountryAddRequest? addRequest2 = new CountryAddRequest() { CountryName = "USA" };

            // 2. Act
            CountryResponse response1 = _countriesService.AddCountry(addRequest1);
            CountryResponse response2 = _countriesService.AddCountry(addRequest2);

            // 3. Assert
            Assert.True(response1.CountryID != Guid.Empty);
            Assert.True(response2.CountryID != Guid.Empty);

            Assert.Contains(response1,_countriesService.GetAllCountries());
            Assert.Contains(response2,_countriesService.GetAllCountries());
        }
        #endregion

        #region Get All Countries
        // The list of countries should be empty by default (before adding any countries to it) .
        [Fact]
        public void GetAllCountries_EmptyList() {
         // Act
         List<CountryResponse> actual_country_response = _countriesService.GetAllCountries();
            // Assert
         Assert.Empty(actual_country_response);
        }

        // The list of countries should contain all the countries that are added to it.
        [Fact]
        public void GetAllCountries_AddFewCountries() {
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
                {new CountryAddRequest()
                { CountryName = "USA" },
                new CountryAddRequest()
                { CountryName = "India" },
                new CountryAddRequest()
                { CountryName = "Europe" }};
            // Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list) {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }

            // Assert
            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();
            foreach (CountryResponse expected_country in countries_list_from_add_country) {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }
        #endregion


        #region Get country by Country ID

        // If we supply null as CountryID, it should return null as CountryResponse
        [Fact]
        public void GetCountryByCountryID_NullCountry() {

            // 1. Arrange
                Guid? countryID = null;
            // 2. Act
                CountryResponse? actual_country_response = _countriesService.GetCountryByCountryID(countryID);
            // 3. Assert
                Assert.Null(actual_country_response);
        }

        // If we supply valid CountryID, it should return the matching country details as CountryResponse object
        [Fact]
        public void GetCountryID_ValidCountryID() {
            // 1. Arrange
               CountryAddRequest? addRequest = new CountryAddRequest() { CountryName = "USA" };
               CountryResponse country_response_from_add_country = _countriesService.AddCountry(addRequest);
            // 2. Act
               Guid countryID = country_response_from_add_country.CountryID;
               CountryResponse? country_response_from_get_country = _countriesService.GetCountryByCountryID(countryID);
            // 3. Assert
               Assert.Equal(country_response_from_add_country,country_response_from_get_country);

        }
        #endregion
    }
}
