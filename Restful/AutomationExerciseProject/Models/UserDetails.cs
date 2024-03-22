using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExerciseProject.Models
{
    public class UserDetails
    {
        public bool IsMale { get; set; }
        public string Password { get; set; }
        public int Days { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Company {  get; set; }
        public string Address {  get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int Number { get; set; }
    }
}
