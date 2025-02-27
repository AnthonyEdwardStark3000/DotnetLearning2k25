using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class used as return type for CountryResponse methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }
    }
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse() { CountryID = country.CountryID,CountryName = country.CountryName };
        }
    }
}
