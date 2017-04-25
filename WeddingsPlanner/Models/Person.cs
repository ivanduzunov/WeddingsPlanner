using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WeddingsPlanner.Models
{
    public class Person
    {
        private string gender;
        [Key]
        public int Id { get; set; }
        [MinLength(1), MaxLength(60)]
        public string FirstName { get; set; }
        public char MiddleNameSymbol { get; set; }
        [MinLength(2)]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleNameSymbol + " " + LastName;
            }
        }
        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                string checkGender = value;
                if (checkGender == "Male" || checkGender == "Female " || checkGender == "Not Specified")
                {
                    this.gender = value;
                }
                else
                {
                    throw new Exception("Invalid Gender!");
                }
            }
        }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - Birthdate.Year;
            }
        }

    }
}
