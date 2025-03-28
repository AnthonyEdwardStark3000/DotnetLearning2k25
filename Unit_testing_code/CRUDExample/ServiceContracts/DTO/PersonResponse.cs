using System;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO class that is used as return type of most methods of Persons Service
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public String? PersonName { get; set; }
        public String? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public String? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public String? Country { get; set; }
        public String? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        /// <summary>
        /// compares the current object data with the parameter object
        /// </summary>
        /// <param name="obj">The person response object to compare</param>
        /// <returns>True or False indicating all person details are matched with the specified parameter object</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;
            return PersonID == person.PersonID && PersonName == person.PersonName && Email == person.Email
                && DateOfBirth == person.DateOfBirth && Gender == person.Gender
                && CountryID == person.CountryID && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters && Age == person.Age;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        }


    public static class PersonExtensions
    {

        // Extension method for person class
        /// <summary>
        /// Extension method to convert Person object to PersonResponse object
        /// </summary>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryID = person.CountryID,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }
}
