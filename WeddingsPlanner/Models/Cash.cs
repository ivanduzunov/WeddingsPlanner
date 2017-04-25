using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WeddingsPlanner.Models
{
    public class Cash
    {
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }
        public double CashAmount { get; set; }
        public int InvitationId { get; set; }
        public virtual Invitation Invitation { get; set; }
    }
}
