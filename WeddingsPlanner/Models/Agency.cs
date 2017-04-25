using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WeddingsPlanner.Models
{
   public  class Agency
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeesCount { get; set; }
        public string Town { get; set; }
    }
}
