using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.ImportJSON
{
    public class PersonDto
    {
        [Key]
        public int Id { get; set; }
        [MinLength(1), MaxLength(60)]
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        [MinLength(2)]
        public string LastName { get; set; }
       
        public DateTime Birthday { get; set; }
        public string Gender
        {
            get; set;

        }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
