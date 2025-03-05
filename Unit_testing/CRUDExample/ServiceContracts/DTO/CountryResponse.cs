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

        //  .Equals() by default compares the references of the objects, and now we are overriding it to
        // compare the values present in the objects.
        // It is advisable to override the GetHashCode() method whenever we override the Equals() method.
        public override bool Equals(object? obj)
        {
            if (obj == null) {
                return false;
            }
            if (obj.GetType()!= typeof(CountryResponse)) {
                return false;
            }

            //Typecasting it to countryResponse type
            CountryResponse country_to_compare = obj as CountryResponse;
            return (this.CountryID == country_to_compare.CountryID && this.CountryName == country_to_compare.CountryName);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();  
        }
    }
    public static class CountryExtensions
    {
        /// <summary>
        /// Converts from country Object to countryResponse Object
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse() { CountryID = country.CountryID,CountryName = country.CountryName };
        }
    }
}
