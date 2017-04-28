using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Models
{
    public class Gift
    {
       
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }
        public string Name { get; set; }
        public string Size
        {
            get; set;
        }

    }
}
