using Entities;

namespace ServiceContracts.DTO
{
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        /// <summary>
        /// creates a new country ID when the name is entered and acts as an DTO class
        /// </summary>
        /// <returns>New Country</returns>
        public Country ToCountry() {
            return new Country() { CountryName = CountryName };
        }
    }
}
