using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO used as return type for most of countries services. 
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        // It compares current object to another object of CountryResponse type and returns true if both are
        // same otherwise false
        public override bool Equals(object? obj)
        {
            if (obj==null) {
                return false;
            }
            if (obj.GetType()!=typeof(CountryResponse)) {
                return false;
            }
            CountryResponse country_to_compare = obj as CountryResponse;
            return (CountryID == country_to_compare.CountryID && CountryName== country_to_compare.CountryName);
        }

        // Overriding Equals method demands to override GetHashCode method as well
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Extension class , will be added to Country Entity
    /// </summary>
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country) {
            return new CountryResponse (){
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}
