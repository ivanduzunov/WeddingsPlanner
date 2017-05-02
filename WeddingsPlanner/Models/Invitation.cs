using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WeddingsPlanner.Models
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; }
        public int? GuestId { get; set; }
        public virtual Person Guest { get; set; }
        public int WeddingId { get; set; }
        public string Present { get; set; }
        public virtual Wedding Wedding { get; set; }
        public bool Attending { get; set; }
        public string Family { get; set; }
    }
}
