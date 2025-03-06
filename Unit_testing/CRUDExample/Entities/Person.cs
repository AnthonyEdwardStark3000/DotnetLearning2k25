using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    // Domain model class Representing the Persons (i.e)Used in the datasource
    public class Person
    {
        public Guid PersonID { get; set; }
        public String? PersonName { get; set; }
        public String? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public String? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public String? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

    }
}
