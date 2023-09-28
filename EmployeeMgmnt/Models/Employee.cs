using System.ComponentModel.DataAnnotations;

namespace EmployeeMgmnt.Models
{
    public class Employee
    {
        [Key]
        public int empid { get; set; }

        public string empfirstname { get; set; } 

        public string emplastname { get; set; }

        public string empphonenumber { get; set; }

        public string empemail { get; set; } 

        public string empgender { get; set; } 


    }
}
