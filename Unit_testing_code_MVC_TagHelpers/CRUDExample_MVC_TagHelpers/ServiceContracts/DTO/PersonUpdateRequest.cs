using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    public class personUpdateRequest
    {
        /// <summary>
        /// DTO used as return type for updating a person services 
        /// </summary>
        [Required(ErrorMessage = "Person ID cannot be blank")]
        public Guid PersonID { get; set; }
        [Required(ErrorMessage = "Patient Name cannot be empty")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Patient Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Email value should be a proper email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public String? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the currrent object of PersonAddRequest into a new object of Person type
        /// </summary>
        /// <returns>Person Object</returns>
        public Person ToPerson()
        {
            return new Person() { PersonID = PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender = Gender.ToString(), CountryID = CountryID, Address = Address, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }
}