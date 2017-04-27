using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.ImportJSON
{
    public class WeddingDto
    {
        public WeddingDto()
        {
            this.Guests = new HashSet<GuestDto>();
        }
        public string Bride { get; set; }
        public string Bridegroom { get; set; }
        public DateTime Date { get; set; }
        public string Agency { get; set; }
        public virtual ICollection<GuestDto> Guests { get; set; }
    }
}

