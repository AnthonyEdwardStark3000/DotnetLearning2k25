using System;
using ServiceContracts.DTO.Enums;


namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as a DTO for inserting a new person
    /// </summary>
    public class PersonAddRequest
    {
        public Guid PersonID { get; set; }
        public String? PersonName { get; set; }
        public String? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public String? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
    }
}
