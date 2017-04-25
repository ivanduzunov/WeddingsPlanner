using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Models
{
    public class Venue
    {
        public Venue()
        {
            this.Weddings = new HashSet<Wedding>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Town { get; set; }
        public virtual ICollection<Wedding> Weddings { get; set; }
    }
}
